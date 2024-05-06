import { Component } from '@angular/core';
import { FormBuilder } from '@angular/forms';

import {
  CoreControlValueAccessor,
} from '../../../../../core/components/core-control-value-accessor/core-control-value-accessor';
import { BaseControlCvaParams } from '../../control-cva/domain/base-control-cva.params';
import { SubformCvaFormTools } from './subform-cva.form-tools';
import { SubformCvaParams } from './subform-cva.params';

@Component({
  selector: 'cw-subform-cva',
  templateUrl: './subform-cva.component.html',
  styleUrl: './subform-cva.component.scss'
})
export class SubformCvaComponent extends CoreControlValueAccessor<object | null>  {

  formTools: SubformCvaFormTools;
  params?: SubformCvaParams;
  inited = false;

  constructor(formBuilder: FormBuilder) {
    super();
    this.formTools = new SubformCvaFormTools(formBuilder);
    this.formTools.ngOnInit();
    this.formTools.form.valueChanges.subscribe((value) => {
      if (this.formTools.form.valid)
        this.registerChange(value);
      else
        this.registerChange(null);
    });
  }

  override setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) {
      this.formTools.form.disable();
    } else {
      this.formTools.form.enable();
    }
  }
  override writeValue(obj: any): void {
    setTimeout(() => {
      this.formTools.form.patchValue(obj);
    }, 100);
  }
  override addParams(params: BaseControlCvaParams): void {
    this.params = params as SubformCvaParams;
  }
  override init(): void {
    setTimeout(() => {
      this.inited = true;
      this.formTools.setControls(this.params!.controlsProvider);
    }, 0);
  }

}
