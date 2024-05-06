import { ValidatorFn } from "@angular/forms";

export class ControlCvaValidation {
  constructor(public readonly message: string, public readonly validator: ValidatorFn, public readonly validationErrorCode: string){}
}
