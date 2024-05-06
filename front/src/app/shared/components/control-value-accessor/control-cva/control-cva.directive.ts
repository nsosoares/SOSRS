import { Directive, ViewContainerRef, forwardRef } from "@angular/core";
import { ControlValueAccessor, FormControlName, NG_VALUE_ACCESSOR } from "@angular/forms";

@Directive({
  selector: '[controlCva]',
  providers: [
    {
    provide: NG_VALUE_ACCESSOR,
    useExisting: forwardRef(() => ControlCvaDirective),
    multi: true,
  },
  ]
})
export class ControlCvaDirective  implements ControlValueAccessor  {

  public content?: ControlValueAccessor | any;
  registerOnChangeGlobal?(fn: any): void;
  registerOnTouchedGlobal?(fn: any): void;

  constructor(public viewContainerRef: ViewContainerRef) {}

  writeValue(obj: any): void {
    this.content?.writeValue(obj);
  }
  registerOnChange(fn: any): void {
    this.registerOnChangeGlobal = fn;
    this.content?.registerOnChange(fn);
  }
  registerOnTouched(fn: any): void {
    this.registerOnTouchedGlobal = fn;
    this.content?.registerOnTouched(fn);
  }
  setDisabledState?(isDisabled: boolean): void {
    const content = this.content as any;
    if (this.content)
      content.setDisabledState(isDisabled);
  }
  writeValueOut(obj: any): void {
    return obj
  }

}
