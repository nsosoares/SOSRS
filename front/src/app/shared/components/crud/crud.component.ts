import { Component, input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { catchError, debounceTime, switchMap, tap } from 'rxjs';

import { AppConstant, RESPONSIVE_SIZE_4 } from '../../../core/constants/app_constants';
import { IEntity } from '../../../core/entities/i-entity';
import { STARTER_ANIMATIONS } from '../../animations/on-show.animations';
import { InputTextCvaParams } from '../control-value-accessor/children/input-text-cva/input-text-cva.params';
import { ControlCvaProvider } from '../control-value-accessor/control-cva/control-cva.provider';
import { CreateFormComponent } from '../forms/create-form/create-form.component';
import { CreateFormParams } from '../forms/create-form/create-form.params';
import { RemoveFormComponent } from '../forms/remove-form/remove-form.component';
import { RemoveFormParams } from '../forms/remove-form/remove-form.params';
import { UpdateFormComponent } from '../forms/update-form/update-form.component';
import { UpdateFormParams } from '../forms/update-form/update-form.params';
import { CrudParams } from './crud.params';
import { INamedEntity } from '../../../core/entities/i-named-entity';

@Component({
  selector: 'cw-crud',
  templateUrl: './crud.component.html',
  styleUrl: './crud.component.scss',
  animations: [STARTER_ANIMATIONS]
})
export class CrudComponent {
  params = input<CrudParams>();
  searching = false;
  form: FormGroup;
  controls = {
    search: ControlCvaProvider.inputText(() => InputTextCvaParams.text('key', 'Buscar', AppConstant.DEFAULT_MAX_LENGTH, 0)),
    id: ControlCvaProvider.inputText(() => InputTextCvaParams.number('id', 'Id').withCssClass(RESPONSIVE_SIZE_4)),
    nome: ControlCvaProvider.inputText(() => InputTextCvaParams.text('nome', 'Nome', 100, 0).withCssClass(RESPONSIVE_SIZE_4)),
    cidade: ControlCvaProvider.inputText(() => InputTextCvaParams.text('cidade', 'Cidade', 100, 0).withCssClass(RESPONSIVE_SIZE_4)),

  }

  constructor(private _dialog: MatDialog) {
    this.form = new FormGroup({
      search: this.controls.search.formControl,
      id: this.controls.id.formControl,
      nome: this.controls.nome.formControl,
      cidade: this.controls.cidade.formControl
    });

    this.form.valueChanges
      .pipe(
        tap(() => this.searching = true),
        debounceTime(300),
        switchMap((formValue) => this.params()!.funcGetEntities(formValue)),
        catchError((error) => {
          this.searching = false;
          console.log(error, 'Erro ao buscar entidades');
          return [];
        })
      )
      .subscribe((searchResult) => {
        this.params()!.listParams.setEntites(searchResult);
        this.searching = false;
      });


  }

  openCreate(): void {
    console.log('this.params()');
    const ref = this._dialog.open(CreateFormComponent, {
      data: {
        formParams: new CreateFormParams(this.params()?.title!, this.params()!.controls, this.params()!.funcCreateEntity)
      },
      maxHeight: '900px',
    });

    ref.afterClosed().subscribe((result) => {
      if (result.saved) {
        this.controls.search.formControl.setValue(this.controls.search.formControl.value);
      }
    });
  }

  openUpdate(entity: IEntity): void {
    const ref = this._dialog.open(UpdateFormComponent, {
      data: {
        formParams: new UpdateFormParams(this.params()?.title!, this.params()!.controls, this.params()!.funcUpdateEntity, entity, this.params()!.funcGetEntity)
      }
    });

    ref.afterClosed().subscribe((result) => {
      if (result.saved) {
        this.controls.search.formControl.setValue(this.controls.search.formControl.value);
      }
    });
  }

  openRemove(entity: INamedEntity): void {
    const ref = this._dialog.open(RemoveFormComponent, {
      data: {
        formParams: new RemoveFormParams(this.params()?.title!, entity, this.params()!.funcRemoveEntity)
      }
    });

    ref.afterClosed().subscribe((result) => {
      if (result.saved) {
        this.controls.search.formControl.setValue(this.controls.search.formControl.value);
      }
    });
  }
}
