import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';

import { Observable, throwError } from 'rxjs';
import { AuthService } from './auth.service';
import {catchError, retry} from 'rxjs/operators';
import {Router} from "@angular/router";

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

    constructor(
        protected authService: AuthService,
        protected router: Router
    ) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const authToken =  this.authService.getToken();

        if (authToken) {
            const authReq = request.clone({ setHeaders: { Authorization: authToken } });
            return next.handle(authReq)
                .pipe(
                    retry(1),
                    catchError(error  => this.handleError(error))
                );
        }
        return next.handle(request);
    }

    private handleError(error: any): Observable<any> {
        if (error instanceof HttpErrorResponse && error.status === 401) {
            this.router.navigate(["/access"]) // TODO Pagina de erros
        }
        return throwError(() =>error);
    }

}
