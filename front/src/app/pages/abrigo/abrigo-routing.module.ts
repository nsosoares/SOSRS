import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AbrigoComponent } from './abrigo.component';
import { AbrigoAjudaComponent } from './abrigo-ajuda/abrigo-ajuda.component';

const routes: Routes = [
  {path: 'abrigo', component: AbrigoComponent, data: {title: 'Abrigo'}},
  {path: 'abrigo-ajuda', component: AbrigoAjudaComponent, data: {title: 'Abrigo'}},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AbrigoRoutingModule {}
