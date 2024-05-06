import { ChangeDetectorRef, Component, forwardRef, input } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, NG_VALUE_ACCESSOR } from '@angular/forms';

import {
  CoreControlValueAccessor,
} from '../../../../../core/components/core-control-value-accessor/core-control-value-accessor';
import { BaseControlCvaParams } from '../../control-cva/domain/base-control-cva.params';
import { InputTextCvaParams } from './input-text-cva.params';
import { ThemePalette } from '@angular/material/core';

@Component({
  selector: 'cw-input-text-cva',
  templateUrl: './input-text-cva.component.html',
  providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => InputTextCvaComponent),
      multi: true,
    },
  ]
})
export class InputTextCvaComponent extends CoreControlValueAccessor<string> {
  formControlNamee = input();
  colorControl = new FormControl('primary' as ThemePalette);
  params?: InputTextCvaParams;

  form: FormGroup;
  control: AbstractControl;

  constructor(formBuilder: FormBuilder, private _changeDetectorRef: ChangeDetectorRef) {
    super();

    this.control = formBuilder.control('');
    this.form = formBuilder.group({
      currentValue: this.control
    });
    this.form.valueChanges.subscribe((value) => {
      this.registerChange(value.currentValue);
    });

  }

  override writeValue(value: any): void {
    this.form.get('currentValue')?.setValue(value);
    this._changeDetectorRef.detectChanges();
  }

  override setDisabledState?(isDisabled: boolean): void {
    if (isDisabled) {
      this.control.disable();
    } else {
      this.control.enable();
    }
  }

  override addParams(params: BaseControlCvaParams): void {
    this.params = params as InputTextCvaParams;
  }
  override init(): void {
    this.control.setValidators(this.params!.getValidations());
    setTimeout(() => {
      this.control.updateValueAndValidity();
    }, 0);
  }

}
