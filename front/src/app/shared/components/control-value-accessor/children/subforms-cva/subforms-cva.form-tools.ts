import { FormBuilder, FormGroup } from '@angular/forms';

import { CoreForm } from '../../../../../core/components/core-form';
import { ControlCvaProvider } from '../../control-cva/control-cva.provider';

export class SubformsCvaFormTools extends CoreForm {

  private _currentControlsCvaProvider: ControlCvaProvider<any,any>[] = [];
  get currentControlsCvaProvider(): ControlCvaProvider<any,any>[] {
    return this._currentControlsCvaProvider;
  }
  constructor(formBuilder: FormBuilder) {
    super(formBuilder);
  }
  override generateForm(formBuilder: FormBuilder): FormGroup<any> {
    return formBuilder.group({});
  }

  setControls(controlsCvaProviderModel: ControlCvaProvider<any,any>[]): void {
    this._currentControlsCvaProvider = controlsCvaProviderModel.map((controlCvaProvider) => controlCvaProvider.clone());
    this.addControlsCvaToForm(this.form, this._currentControlsCvaProvider);
  }
}
