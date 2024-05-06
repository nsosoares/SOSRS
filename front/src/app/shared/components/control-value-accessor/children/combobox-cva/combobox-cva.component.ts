import { Component, forwardRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';
import { debounceTime, Observable, of, switchMap } from 'rxjs';

import {
  CoreControlValueAccessor,
} from '../../../../../core/components/core-control-value-accessor/core-control-value-accessor';
import { INamedEntity } from '../../../../../core/entities/i-named-entity';
import { BaseControlCvaParams } from '../../control-cva/domain/base-control-cva.params';
import { ComboboxCvaParams } from './combobox-cva.params';
import { ComboboxService } from './combobox.service';

@Component({
  selector: 'cw-combobox-cva',
  templateUrl: './combobox-cva.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ComboboxCvaComponent),
      multi: true,
    },
  ]
})
export class ComboboxCvaComponent extends CoreControlValueAccessor<any> {

  readonly form: FormGroup;
  readonly currentValueControl: FormControl = new FormControl('');
  readonly searchControl: FormControl = new FormControl('');
  readonly comboboxService = new ComboboxService();
  selectedEntity: INamedEntity = { id: '', name: '' };

  constructor(formBuilder: FormBuilder) {
    super();
    this.form = formBuilder.group({
      search: this.searchControl,
      currentValueControl: this.currentValueControl,
    });

    this.searchControl.valueChanges.pipe(
      debounceTime(350)
      , switchMap((searchTerm: string) => this.comboboxService.searchByKeyword(searchTerm))
      , switchMap((searchResult) => this._getEntityResultFromSearch(searchResult))
    ).subscribe((entityResult) => {
      this.selectedEntity = entityResult;
      this.currentValueControl.setValue(entityResult.id);
      this.registerChange(entityResult.id);
    });
  }

  private _getEntityResultFromSearch(searchResult: INamedEntity[]): Observable<INamedEntity>{
    const searchTerm = this.searchControl.value;
    if (searchResult.length === 0) return of(NENHUMA_OPCAO_ENCONTRADA);

    const findedEntity = searchResult.find(entity => entity.name === searchTerm);
    if (findedEntity) return of(findedEntity);

    return of(NENHUMA_OPCAO_ENCONTRADA);
  }

  forceSearch(): void {
    this.searchControl.setValue(this.searchControl.value);
  }

  validDisplayValueOnFocusOut(): void {
    setTimeout(() => {
      if (this.selectedEntity.id) return;
      this.comboboxService.desableSearchToNextCall();
      this.searchControl.setValue('');
    }, 5);

  }

  override writeValue(id: any): void {
    this.selectedEntity.id = id;
    this.loadCurrentEntity();
  }
  override setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) this.searchControl.disable();
    else this.searchControl.enable();
  }
  override addParams(params: BaseControlCvaParams): void {
    this.comboboxService.setParams(params as ComboboxCvaParams);
  }
  override init(): void {
    this.searchControl.setValidators(this.comboboxService.params!.getValidations());
    this.currentValueControl.setValidators(this.comboboxService.params!.getValidations());
    this.form.updateValueAndValidity();
    this.loadCurrentEntity();
  }

  loadCurrentEntity(): void {
    if (!this.comboboxService.params || !this.selectedEntity?.id) return;

    this.comboboxService.getEntityById(this.selectedEntity.id).subscribe((selectedEntity) => {
      this.selectedEntity = selectedEntity;
      this.searchControl.setValue(selectedEntity.name);
    });

  }
}
const NENHUMA_OPCAO_ENCONTRADA: INamedEntity = { id: null, name: 'Nenhuma opção encontrada' }
