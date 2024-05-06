import { Component } from '@angular/core';
import { TemplateService } from './template/template.service';

@Component({
  selector: 'cw-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {

  constructor(public service: TemplateService) {

  }

  exibeMenu(): boolean {
    return this.service.exibeMenu;
  }
}
