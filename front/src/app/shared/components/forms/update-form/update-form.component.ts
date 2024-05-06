import { Component, Inject } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

import { CoreForm } from '../../../../core/components/core-form';
import { UpdateFormParams } from './update-form.params';

@Component({
  selector: 'cw-update-form',
  templateUrl: './update-form.component.html',
  styleUrl: './update-form.component.scss'
})
export class UpdateFormComponent extends CoreForm {

  params: UpdateFormParams;
  constructor(
    formBuilder: FormBuilder,
    public dialogRef: MatDialogRef<UpdateFormComponent>,
    @Inject(MAT_DIALOG_DATA) params: any,
  ) {
    super(formBuilder);
    this.params = params.formParams;
  }

  override generateForm(formBuilder: FormBuilder): FormGroup<any> {
    const form = formBuilder.group({});
    this.addControlsCvaToForm(form, this.params.controlsCvaProvider);
    this.params.funcGetEntityByIdFunc(this.params.entity.id!).subscribe({
      next: (entity) => {
        console.log('entity', entity);
        setTimeout(() => {
          form.patchValue(entity);
        }, 500);
      }
    });
    return form;
  }

  finishSave(): void {
    this.dialogRef.close({ saved: true });
  }
}
