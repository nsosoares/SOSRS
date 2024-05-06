import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { Usuario } from './usuario.model';

@Injectable({
  providedIn: 'root'
})
export class UsuarioService {
  baseUrlLocal = environment.api;
  baseUrl = this.baseUrlLocal;

constructor() { }
cadastrarUsuario(usuario: Usuario): void {
  // Lógica para cadastrar o usuário (por exemplo, enviar para um servidor)
  console.log('Cadastrando usuário:', usuario);
}

}
