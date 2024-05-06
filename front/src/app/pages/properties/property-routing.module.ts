import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PropertyComponent } from './property/property.component';
import { PropertyCreateComponent } from './property/property-create/property-create.component';

const routes: Routes = [
  {path: 'imovel', component: PropertyComponent, data: {title: 'Imóvel'}},
  {path: 'imovel/criar', component: PropertyCreateComponent, data: {title: 'Imóvel - criar'}},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ImoveisRoutingModule {}
