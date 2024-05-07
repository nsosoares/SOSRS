import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Usuario } from './usuario.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  baseUrlLocal = environment.api;
  baseUrl = this.baseUrlLocal;

  constructor(private _httpClient: HttpClient) { }
  cadastrarUsuario = (usuario: Usuario): Observable<any> => {
    return this._httpClient.post<any>(this.baseUrl + 'auth/register', usuario);
  }
}
