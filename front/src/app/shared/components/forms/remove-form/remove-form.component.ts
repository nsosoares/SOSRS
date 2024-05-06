import { Component, Inject } from '@angular/core';
import { RemoveFormParams } from './remove-form.params';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'cw-remove-form',
  templateUrl: './remove-form.component.html',
  styleUrl: './remove-form.component.scss'
})
export class RemoveFormComponent {

  params: RemoveFormParams;
  constructor(
    public dialogRef: MatDialogRef<RemoveFormComponent>,
    @Inject(MAT_DIALOG_DATA) params: any,
  ) {
    this.params = params.formParams;
  }

  remove(): void {
    this.params.removeFunc(this.params.entity.id!).subscribe({
      next: () => {
        this.finishSave();
      }
    });
  }

  finishSave(): void {
    this.dialogRef.close({ saved: true });
  }


  onlyClose(): void {
    this.dialogRef.close({ saved: false });
  }
}
