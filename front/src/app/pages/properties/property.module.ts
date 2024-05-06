import { LOCALE_ID, NgModule } from '@angular/core';
import { PropertyComponent } from './property/property.component';
import { ImoveisRoutingModule } from './property-routing.module';
import { CoreModule } from '../../core/core.module';
import { PropertyCreateComponent } from './property/property-create/property-create.component';
import { SharedModule } from '../../shared/shared.module';



@NgModule({
  declarations: [
    PropertyComponent,
    PropertyCreateComponent
  ],
  imports: [
    CoreModule,
    SharedModule,
    ImoveisRoutingModule,
  ],
  providers: [
    { provide: LOCALE_ID, useValue: 'pt-BR' }
  ]
})
export class PropertyModule { }
