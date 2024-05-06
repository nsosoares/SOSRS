import { Component, input } from '@angular/core';

@Component({
  selector: 'cw-btn-remove',
  templateUrl: './btn-remove.component.html',
  styleUrl: './btn-remove.component.scss'
})
export class BtnRemoveComponent {

  useDescribe = input<boolean>(true);
}
