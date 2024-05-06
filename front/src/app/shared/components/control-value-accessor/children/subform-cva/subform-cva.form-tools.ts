import { FormBuilder, FormGroup } from '@angular/forms';

import { CoreForm } from '../../../../../core/components/core-form';
import { ControlCvaProvider } from '../../control-cva/control-cva.provider';

export class SubformCvaFormTools extends CoreForm {

  constructor(formBuilder: FormBuilder) {
    super(formBuilder);
  }
  override generateForm(formBuilder: FormBuilder): FormGroup<any> {
    return formBuilder.group({});
  }

  setControls(controlsCvaProvider: ControlCvaProvider<any,any>[]): void {
    this.addControlsCvaToForm(this.form, controlsCvaProvider);
  }
}
