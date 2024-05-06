import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';

import {
  CoreControlValueAccessor,
} from '../../../../../core/components/core-control-value-accessor/core-control-value-accessor';
import { AppError } from '../../../../../core/erros/app-error';
import { BaseControlCvaParams } from '../../control-cva/domain/base-control-cva.params';
import { SubformsCvaFormTools } from './subforms-cva.form-tools';
import { SubformsCvaParams } from './subforms-cva.params';
import { STARTER_ANIMATIONS } from '../../../../animations/on-show.animations';
import { Subject, debounceTime } from 'rxjs';

@Component({
  selector: 'cw-subforms-cva',
  templateUrl: './subforms-cva.component.html',
  styleUrl: './subforms-cva.component.scss',
  animations: [STARTER_ANIMATIONS]

})
export class SubformsCvaComponent extends CoreControlValueAccessor<any[] | null>{

  formTools: SubformsCvaFormTools[] = [];
  params?: SubformsCvaParams;
  inited = false;

  constructor() {
    super();

    this.emmitAddValue.pipe(
      debounceTime(100)
    ).subscribe((obj) => {

      setTimeout(() => {
        obj.forEach((value, index) => {
          this.newForm();
          this.formTools[index].form.patchValue(value);
        });
      }, 100);
    })
  }
  newForm(): void {
    const newFormTools = new SubformsCvaFormTools(new FormBuilder());
    newFormTools.ngOnInit();
    newFormTools.setControls(this.params!.controlsProvider);
    newFormTools.form.valueChanges.subscribe(() => {
      if (this.hasAnyFormInvalid())
        this.registerChange(null);
      else
        this.registerChange(this._getValueFormAllForms());
    });
    console.log('adicionando:', newFormTools.form)
    this.formTools.push(newFormTools);
  }

  removeForm(form: SubformsCvaFormTools): void {
    this.formTools = this.formTools.filter(formTools => formTools !== form);
    this.registerChange(this._getValueFormAllForms());
  }

  hasAnyFormInvalid(): boolean {
    return this.formTools.some(formTools => formTools.form.invalid);
  }

  anyFormHasTouchedOrDirty(): boolean {
    return this.formTools.some(formTools => formTools.form.dirty );
  }
  private _getValueFormAllForms(): any[] {
    return this.formTools.map(formTools => formTools.form.value);
  }
  private _resetForm(): void {
    this.formTools = [];
  }
  override setDisabledState?(isDisabled: boolean): void {
    if (isDisabled)
      this.formTools.forEach(formTools => formTools.form.disable());
    else
      this.formTools.forEach(formTools => formTools.form.enable());
  }
  override writeValue(obj: any): void {
    this._resetForm();


    if (!obj) return;
    if (!Array.isArray(obj)) throw AppError.fromBussiness('o vamor em subforms-cva deve ser um array, valor atual:', obj);

    this.emmitAddValue.next(obj);
  }

  emmitAddValue: Subject<any[]> = new Subject();
  override addParams(params: BaseControlCvaParams): void {
    this.params = params as SubformsCvaParams;
  }
  override init(): void {
    setTimeout(() => {
      this.formTools.forEach(formTools => formTools.setControls(this.params!.controlsProvider));
      this.inited = true;
    }, 0);
  }

}
