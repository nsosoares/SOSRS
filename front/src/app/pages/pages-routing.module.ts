import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthGuard } from './auth/auth.guard';

@NgModule({
  imports: [
    RouterModule.forChild([
      {
        path: '',
        redirectTo: 'abrigos/abrigo-ajuda',
        pathMatch: 'full',
      },
      {
        path: 'properties',
        canActivate: [AuthGuard],
        loadChildren: () =>
          import('./properties/property.module').then((m) => m.PropertyModule),
      },
      {
        path: 'abrigos',
        canActivate: [AuthGuard],
        loadChildren: () =>
          import('./abrigo/abrigo.module').then((m) => m.AbrigoModule),
      },

      { path: '**', redirectTo: '/notfound' },
    ]),
  ],
  exports: [RouterModule],
})
export class PagesRoutingModule {}
