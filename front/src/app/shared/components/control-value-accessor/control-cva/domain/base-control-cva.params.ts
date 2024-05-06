import { ValidatorFn, Validators } from "@angular/forms";
import { ControlCvaValidation } from "./control-cva.vlidation";
import { Observable, Subject } from "rxjs";
import { RESPONSIVE_SIZE_3 } from "../../../../../core/constants/app_constants";

export abstract class BaseControlCvaParams {
  private _cssClass = RESPONSIVE_SIZE_3;
  get cssClass(): string {
    return this._cssClass;
  }

  readonly validations: ControlCvaValidation[] = [];

  private readonly _onChangeValidations = new Subject<ControlCvaValidation[]>();
  get onChangeValidations(): Observable<ControlCvaValidation[]> {
    return this._onChangeValidations.asObservable();
  }

  private _defaultValue: any;
  get defaultValue(): object | number | string {
    if (this._defaultValue === undefined)
      return '';
    return this._defaultValue;
  }

  protected constructor(public readonly controlName: string, public readonly label: string) {
  }

  asRequired(): this {
    this.validations.push(new ControlCvaValidation('Obrigatório', Validators.required, 'required'));
    return this;
  }
  withValidations(validations: ControlCvaValidation[]): this {
    this.validations.push(...validations);
    this._onChangeValidations.next(this.validations);
    return this;
  }

  replaceValidations(validations: ControlCvaValidation[]): void {
    this.validations.length = 0;
    this.withValidations(validations);
  }

  withDefaultValue(defaultValue: object | number | string): this {
    this._defaultValue = defaultValue;
    return this;
  }

  getValidations(): ValidatorFn[] {
    return this.validations.map((validation) => validation.validator);
  }

  withCssClass(cssClass: string): this {
    this._cssClass = cssClass;
    return this;
  }
}
