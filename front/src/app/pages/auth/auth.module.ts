import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthRoutingModule } from './auth-routing.module';
import { AuthService } from './auth.service';
import { httpInterceptorProviders } from './ httpIntercptorProviders';
import { AuthInterceptor } from './auth.interceptor';
import { AuthGuardService } from './auth.guard';

@NgModule({
    providers: [
        AuthGuardService,
        AuthService,
        AuthInterceptor,
        httpInterceptorProviders
      ],
    imports: [
        CommonModule,
        AuthRoutingModule
    ]
})
export class AuthModule { }
