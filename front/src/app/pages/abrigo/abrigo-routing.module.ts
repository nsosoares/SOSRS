import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AbrigoComponent } from './abrigo.component';
import { AbrigoAjudaComponent } from './abrigo-ajuda/abrigo-ajuda.component';
import { AuthGuard } from '../auth/auth.guard';

const routes: Routes = [

  {path: 'abrigo-adm-hash', component: AbrigoComponent, data: {title: 'Abrigo'}, canActivate: [AuthGuard],},
  {path: 'abrigo-ajuda', component: AbrigoAjudaComponent, data: {title: 'Abrigo'}},
  {path: 'abrigo-ajuda/animais', component: AbrigoAjudaComponent, data: {title: 'Abrigo'}},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AbrigoRoutingModule {}
