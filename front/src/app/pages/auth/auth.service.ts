import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Roles } from './Roles';
import { JwtHelperService } from '@auth0/angular-jwt';
import { environment } from '../../../environments/environment';
import { MatSnackBar } from '@angular/material/snack-bar';

const httpOptions = {
  observe: 'response' as const,
};

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly TOKEN = 'token';
  private readonly AUTHORIZATION = 'authorization';
  private url = environment.api;
  private jwtHelperService = new JwtHelperService();

  constructor(
    protected http: HttpClient,
    private _snackBar: MatSnackBar
  ) { }

  login(user: { user: string; password: string }): Observable<boolean> {
    return this.http.post<any>(`${this.url}api/auth/login`, user, httpOptions).pipe(
      tap((res) => {
        this.storeToken(res.body.token);
      }),
      map(() => true),
      catchError((error) => {
        return of(false);
      })
    );
  }

  logout() {
    this.removeToken();
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN);
  }

  isLoggedIn() {
    console.log(this.getToken());
    console.log(this.jwtHelperService.isTokenExpired(this.getToken()));
    return (
      !!this.getToken() &&
      !this.jwtHelperService.isTokenExpired(this.getToken())
    );
  }

  hasAnyPermission(roles: Roles[]) {
    if (roles) {
      return roles.some((r) => this.hasPermission(r));
    }
    return false;
  }

  hasPermission(role: Roles): boolean {
    return this.authorities.includes(role);
  }

  getUserLogged() {
    const email = this.jwtHelperService.decodeToken(this.getToken()).sub;
    let user = {};
    return this.http
      .get<any>(`${this.url}user/email/${email}`);

  }

  private get authorities() {
    return this.jwtHelperService.decodeToken(this.getToken()).authorities;
  }

  private storeToken(token: any) {
    localStorage.setItem(this.TOKEN, token);
  }

  private removeToken() {
    localStorage.removeItem(this.TOKEN);
  }

  message(message: string) {
    this._snackBar.open(message, 'Undo', { duration: 3000 });
  }

}
