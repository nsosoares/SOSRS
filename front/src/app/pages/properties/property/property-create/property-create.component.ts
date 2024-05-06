import { ChangeDetectorRef, Component } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';

import { CoreForm } from '../../../../core/components/core-form';
import {
  ComboboxCvaParams,
} from '../../../../shared/components/control-value-accessor/children/combobox-cva/combobox-cva.params';
import {
  InputTextCvaParams,
} from '../../../../shared/components/control-value-accessor/children/input-text-cva/input-text-cva.params';
import {
  SubformCvaParams,
} from '../../../../shared/components/control-value-accessor/children/subform-cva/subform-cva.params';
import { ControlCvaProvider } from '../../../../shared/components/control-value-accessor/control-cva/control-cva.provider';
import { PropertyService } from '../property.service';

@Component({
  selector: 'cw-property-create',
  templateUrl: './property-create.component.html',
  styleUrl: './property-create.component.scss'
})
export class PropertyCreateComponent extends CoreForm {

  controls: ControlCvaProvider<any,any>[] =  [];
  nameControl = ControlCvaProvider.inputText(() => InputTextCvaParams.text('name', 'Name', 50, 5).asRequired().withPlaceholder('Digite o name do imóvel'));
  subnameControl = ControlCvaProvider.combobox(() => new ComboboxCvaParams('subname', 'Imóvel', this.imovelService.getById, this.imovelService.searchByName).asRequired());
  sub1Control = ControlCvaProvider.inputText(() => InputTextCvaParams.text('sub1', 'sub1', 50, 5).asRequired().withPlaceholder('Digite o name do imóvel'));
  sub2Control = ControlCvaProvider.inputText(() => InputTextCvaParams.text('sub2', 'sub2', 50, 5).asRequired().withPlaceholder('Digite o name do imóvel'));

  pronto = false;
  constructor(formBuilder: FormBuilder, private imovelService: PropertyService, private _changeDetectorRef: ChangeDetectorRef) {
    super(formBuilder);
    const subformControl = ControlCvaProvider.subforms(() => new SubformCvaParams('subform', 'Subform', [this.sub1Control, this.sub2Control]));
    this.controls = [this.nameControl, this.subnameControl, subformControl];
  }

  override generateForm(formBuilder: FormBuilder): FormGroup<any> {
    const form = formBuilder.group({});
    this.addControlsCvaToForm(form, this.controls);
    form.valueChanges.subscribe((value) => {
      console.log(value);
    });

    setTimeout(() => {
      this.pronto = true;
    }, 500);
    return form;
  }

  submit(): void {
    console.log(this.form.value, 'form');
    if (this.form.invalid) return;
  }
}
