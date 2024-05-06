import { Directive, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';

import { ControlCvaProvider } from '../../shared/components/control-value-accessor/control-cva/control-cva.provider';

@Directive()
export abstract class CoreForm implements OnInit {
  form!: FormGroup;

  constructor(protected formBuilder: FormBuilder) {
  }

  abstract generateForm(formBuilder: FormBuilder): FormGroup<any>;

  protected addControlCvaToForm(form: FormGroup, control: ControlCvaProvider<any, any>): void {
    form.addControl(control.params.controlName, control.formControl);
  }

  protected addControlsCvaToForm(form: FormGroup, controls: ControlCvaProvider<any, any>[]): void {

    for (const control of controls) {
      let formControl: FormControl;
      formControl = control.formControl;
      form.addControl(control.params.controlName, formControl);
    }
  }

  ngOnInit(): void {
    this.form = this.generateForm(this.formBuilder);
    this.ngOnInitTemplateMethod();
    this.form.reset();
  }

  ngOnInitTemplateMethod(): void {
  }
}
