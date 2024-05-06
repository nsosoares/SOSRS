import { Component, input } from '@angular/core';

@Component({
  selector: 'cw-btn-add',
  templateUrl: './btn-add.component.html',
  styleUrl: './btn-add.component.scss'
})
export class BtnAddComponent {

  describe = input<string>();
}
