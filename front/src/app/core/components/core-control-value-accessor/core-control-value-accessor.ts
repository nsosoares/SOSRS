import { Directive } from "@angular/core";
import { ControlValueAccessor } from "@angular/forms";
import { BaseControlCvaParams } from "../../../shared/components/control-value-accessor/control-cva/domain/base-control-cva.params";

@Directive()
export abstract class CoreControlValueAccessor<TValueType>  implements ControlValueAccessor{
  private _registerChangeFunc?: (value: any) => void;
  protected registrarToqueDelegate?: (value: any) => void;


  registerOnTouched(fn: () => void) {
    this.registrarToqueDelegate = fn;
  }

  registerOnChange(fn: any): void {
    this._registerChangeFunc = fn;
  }

  protected registerChange(value: TValueType): void {
    if (this._registerChangeFunc) this._registerChangeFunc(value);
  }

  abstract setDisabledState?(isDisabled: boolean): void;
  abstract writeValue(obj: any): void;
  abstract addParams(params: BaseControlCvaParams): void;
  abstract init(): void;
}
