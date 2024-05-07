import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './pages/auth/auth.guard';

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
    path: 'properties',
    canActivate: [AuthGuard],
    loadChildren: () =>
      import('./pages/properties/property.module').then((m) => m.PropertyModule),
  },
  {
    path: 'abrigos',
    loadChildren: () =>
      import('./pages/abrigo/abrigo.module').then((m) => m.AbrigoModule),
  },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
