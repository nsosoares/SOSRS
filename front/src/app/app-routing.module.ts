
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './pages/auth/auth.guard';
import { CadastrarUsuarioComponent } from './pages/cadastrar-usuario/cadastrar-usuario.component';

const routes: Routes = [
  { path: '', redirectTo: 'abrigos/abrigo-ajuda', pathMatch: 'full' },
  { path: 'properties', loadChildren: () => import('./pages/properties/property.module').then(m => m.PropertyModule) },
  { path: 'abrigos', loadChildren: () => import('./pages/abrigo/abrigo.module').then(m => m.AbrigoModule) },
  { path: 'cadastrar-usuario', component: CadastrarUsuarioComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
