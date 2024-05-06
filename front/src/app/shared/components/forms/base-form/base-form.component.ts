import { Component, EventEmitter, input, Output } from '@angular/core';
import { CreateFormParams } from '../create-form/create-form.params';
import { FormBuilder, FormGroup } from '@angular/forms';
import { CoreForm } from '../../../../core/components/core-form';
import { debounceTime, tap } from 'rxjs';

@Component({
  selector: 'cw-base-form',
  templateUrl: './base-form.component.html'
})
export class BaseFormComponent extends CoreForm {


  errorOnSave = false;
  params = input<CreateFormParams>();
  @Output() onSave = new EventEmitter<void>();

  constructor(formBuilder: FormBuilder) {
    super(formBuilder);
  }
  override generateForm(formBuilder: FormBuilder): FormGroup<any> {
    const form = formBuilder.group({});
    this.addControlsCvaToForm(form, this.params()!.controlsCvaProvider);
    return form;
  }

  save(): void {
    if (this.form.invalid) {
      console.log('form invalid');
      return;
    }

    this.params()!.submitFunc(this.form.value).pipe(
      debounceTime(100),
      tap(() => this.errorOnSave = false)
      ).subscribe({
        next: () => {
          this.onSave.emit();
        },
        error: (error) => {
          console.log(error);
          this.errorOnSave = true;
        }
      });
  }
}
