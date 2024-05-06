import { Validators } from '@angular/forms';

import { AppConstant } from '../../../../../core/constants/app_constants';
import { BaseControlCvaParams } from '../../control-cva/domain/base-control-cva.params';
import { ControlCvaValidation } from '../../control-cva/domain/control-cva.vlidation';

export class InputTextCvaParams extends BaseControlCvaParams {

  protected _placeholder = '';
  get placeholder(): string {
    return this._placeholder;
  }

  isHidden(): boolean {
    return this.type === HIDDEN_VALUE;
  }
  public constructor(name: any, label: string, public readonly type: string) {
    super(name, label);
  }

  static text(name: any, label: string, maxLength: number | null = AppConstant.DEFAULT_MAX_LENGTH, minLength: number | null = AppConstant.DEFAULT_MIN_LENGTH): InputTextCvaParamsTextExtension {
    const validations: ControlCvaValidation[] = [];

    if (minLength)
      InputTextCvaParams._addMinLengthValidation(validations, minLength);

    if (maxLength)
      InputTextCvaParams._addMaxLengthValidation(validations, maxLength);

    return new InputTextCvaParamsTextExtension(name, label, 'text').withValidations(validations);
  }

  private static _addMaxLengthValidation(validations: ControlCvaValidation[], maxLength: number) {
    validations.push(new ControlCvaValidation(`máximo ${maxLength} caracteries`, Validators.maxLength(maxLength), 'maxlength'));
  }

  private static _addMinLengthValidation(validations: ControlCvaValidation[], minLength: number) {
    validations.push(new ControlCvaValidation(`mínimo ${minLength} caracteries`, Validators.minLength(minLength), 'minlength'));
  }
  static hidden(name: any): InputTextCvaParams {
    return new InputTextCvaParams(name, '', HIDDEN_VALUE).withCssClass('');
  }

  static number(name: any, label: string): InputTextCvaParams {
    return new InputTextCvaParams(name, label, 'number');
  }
  static email(name: any, label: string): InputTextCvaParams {
    const validations: ControlCvaValidation[] = [];

    validations.push(new ControlCvaValidation('Email inválido', Validators.email, 'email'))
    InputTextCvaParams._addMaxLengthValidation(validations, AppConstant.DEFAULT_MAX_LENGTH);
    InputTextCvaParams._addMaxLengthValidation(validations, AppConstant.DEFAULT_MIN_LENGTH);

    return new InputTextCvaParams(name, label, 'email').withValidations(validations);
  }
}

class InputTextCvaParamsTextExtension extends InputTextCvaParams {
  constructor(name: any, label: string, type: string) {
    super(name, label, type);
  }

  withPlaceholder(placeholder: string): this {
    this._placeholder = placeholder;
    return this;
  }

}

const HIDDEN_VALUE = 'hidden';
