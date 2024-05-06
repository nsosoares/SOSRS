import { UsuarioModule } from './pages/cadastrar-usuario/usuario.module';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', redirectTo: 'abrigos/abrigo-ajuda', pathMatch: 'full' },
  { path: 'properties', loadChildren: () => import('./pages/properties/property.module').then(m => m.PropertyModule) },
  { path: 'abrigos', loadChildren: () => import('./pages/abrigo/abrigo.module').then(m => m.AbrigoModule) },
  { path: 'usuario', loadChildren: () => import('./pages/cadastrar-usuario/usuario.module').then(m => m.UsuarioModule) },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
