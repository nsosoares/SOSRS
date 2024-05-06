import { Component, EventEmitter, Output, input } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'cw-btn-save',
  templateUrl: './btn-save.component.html',
  styleUrl: './btn-save.component.scss'
})
export class BtnSaveComponent {

  form = input<FormGroup>();
  @Output() onSave = new EventEmitter<void>();
}
