
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './pages/auth/auth.guard';
import { CadastrarUsuarioComponent } from './pages/cadastrar-usuario/cadastrar-usuario.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'abrigos/abrigo-ajuda',
    pathMatch: 'full',
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('./pages/auth/auth.module').then((m) => m.AuthModule),
  },
  {
    path: 'abrigos',
    loadChildren: () =>
      import('./pages/abrigo/abrigo.module').then((m) => m.AbrigoModule),
  },

  { path: '', redirectTo: 'abrigos/abrigo-ajuda', pathMatch: 'full' },
  { path: 'abrigos', loadChildren: () => import('./pages/abrigo/abrigo.module').then(m => m.AbrigoModule) },
  { path: 'cadastrar-usuario', component: CadastrarUsuarioComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
