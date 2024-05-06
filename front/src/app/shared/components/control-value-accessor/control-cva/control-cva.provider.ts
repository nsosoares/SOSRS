import { FormControl } from "@angular/forms";
import { AppError } from "../../../../core/erros/app-error";
import { Type } from "@angular/core";
import { InputTextCvaParams } from "../children/input-text-cva/input-text-cva.params";
import { InputTextCvaComponent } from "../children/input-text-cva/input-text-cva.component";
import { ComboboxCvaParams } from "../children/combobox-cva/combobox-cva.params";
import { ComboboxCvaComponent } from "../children/combobox-cva/combobox-cva.component";
import { SubformCvaParams } from "../children/subform-cva/subform-cva.params";
import { SubformCvaComponent } from "../children/subform-cva/subform-cva.component";
import { CoreControlValueAccessor } from "../../../../core/components/core-control-value-accessor/core-control-value-accessor";
import { BaseControlCvaParams } from "./domain/base-control-cva.params";
import { SubformsCvaParams } from "../children/subforms-cva/subforms-cva.params";
import { SubformsCvaComponent } from "../children/subforms-cva/subforms-cva.component";

export class ControlCvaProvider<TControlValueAccessor extends CoreControlValueAccessor<any>, TParams extends BaseControlCvaParams> {
  readonly params?: TParams;

  private _formControl?: FormControl;
  get formControl(): FormControl {
    if (!this._formControl) throw new AppError('Form control não inicializado');
    return this._formControl;
  }

  constructor(public readonly componentCvaType: Type<TControlValueAccessor>, getParamsFunc: () => TParams | undefined){
    if (!getParamsFunc) return;
    this.params = getParamsFunc();
    this._formControl = this.newFormControl();
    this.params?.onChangeValidations.subscribe((validations) => {
      this._formControl!.setValidators(validations.map(v => v.validator));
      this._formControl!.updateValueAndValidity();
    });
  }

  newFormControl(): FormControl {
    return new FormControl(this.params?.defaultValue, this.params?.getValidations());
  }
  static inputText(getParamsFunc: () => InputTextCvaParams | undefined): ControlCvaProvider<InputTextCvaComponent, InputTextCvaParams> {
    return new ControlCvaProvider(InputTextCvaComponent, getParamsFunc);
  }
  static combobox(getParamsFunc: () => ComboboxCvaParams | undefined): ControlCvaProvider<ComboboxCvaComponent, ComboboxCvaParams> {
    return new ControlCvaProvider(ComboboxCvaComponent, getParamsFunc );
  }

  static subform(getParamsFunc: () => SubformCvaParams | undefined): ControlCvaProvider<SubformCvaComponent, SubformCvaParams> {
    return new ControlCvaProvider(SubformCvaComponent, getParamsFunc);
  }
  static subforms(getParamsFunc: () => SubformsCvaParams | undefined): ControlCvaProvider<SubformsCvaComponent, SubformsCvaParams> {
    return new ControlCvaProvider(SubformsCvaComponent, getParamsFunc);
  }

  clone(): ControlCvaProvider<TControlValueAccessor, TParams> {
    return new ControlCvaProvider(this.componentCvaType, () => this.params as TParams);
  }
}
