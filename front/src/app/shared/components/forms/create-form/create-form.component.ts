import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CreateFormParams } from './create-form.params';
import { CoreForm } from '../../../../core/components/core-form';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'cw-create-form',
  templateUrl: './create-form.component.html',
  styleUrl: './create-form.component.scss'
})
export class CreateFormComponent extends CoreForm {

  params: CreateFormParams;
  constructor(
    formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<CreateFormComponent>,
    @Inject(MAT_DIALOG_DATA) params: any,
  ) {
    super(formBuilder);
    this.params = params.formParams;
    console.log(params.formParams);
  }

  override generateForm(formBuilder: FormBuilder): FormGroup<any> {
    const form = formBuilder.group({});
    this.addControlsCvaToForm(form, this.params.controlsCvaProvider);
    form.reset();
    return form;
  }

  finishSave(): void {
    this.dialogRef.close({ saved: true });
  }
}
