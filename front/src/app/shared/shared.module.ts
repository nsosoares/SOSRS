import { NgModule } from '@angular/core';

import { CoreModule } from '../core/core.module';
import { BUTTON_MODULE } from './components/buttons/button.module';
import { ComboboxCvaComponent } from './components/control-value-accessor/children/combobox-cva/combobox-cva.component';
import { InputTextCvaComponent } from './components/control-value-accessor/children/input-text-cva/input-text-cva.component';
import { SubformCvaComponent } from './components/control-value-accessor/children/subform-cva/subform-cva.component';
import { SubformsCvaComponent } from './components/control-value-accessor/children/subforms-cva/subforms-cva.component';
import { ControlCvaComponent } from './components/control-value-accessor/control-cva/control-cva.component';
import { ControlCvaDirective } from './components/control-value-accessor/control-cva/control-cva.directive';
import { CrudListComponent } from './components/crud-list/crud-list.component';
import { CrudComponent } from './components/crud/crud.component';
import { CW_FORMS_MODULE } from './components/forms/form.module';

const globals = [
  ControlCvaComponent,
  CrudComponent,
  CrudListComponent,
  ...CW_FORMS_MODULE,
  ...BUTTON_MODULE
];

const intenals = [
  ...globals,
  SubformsCvaComponent,
  SubformCvaComponent,
  InputTextCvaComponent,
  ControlCvaDirective,
  ComboboxCvaComponent,
];
@NgModule({
  declarations: intenals,
  exports: globals,
  imports: [
    CoreModule
  ],
})
export class SharedModule { }
