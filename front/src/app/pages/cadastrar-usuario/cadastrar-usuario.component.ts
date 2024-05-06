import { Component, OnInit } from '@angular/core';
import { Usuario } from './core/usuario.model';
import { UsuarioService } from './core/usuario.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-cadastrar-usuario',
  templateUrl: './cadastrar-usuario.component.html',
  styleUrls: ['./cadastrar-usuario.component.css']
})
export class CadastrarUsuarioComponent implements OnInit {
  form: FormGroup;


  constructor(private _usuarioService: UsuarioService, fb: FormBuilder) {
    this.form = fb.group({
      nome: ['', Validators.required],
      senha: ['', Validators.required],
      cpf: ['', Validators.required]
    });
  }

  ngOnInit() {
  }
  cadastrarUsuario(): void {
    if (this.form.valid) {
      const usuario: Usuario = this.form.value;
      this._usuarioService.cadastrarUsuario(usuario);
      this.form.reset();
    }
  }
}
