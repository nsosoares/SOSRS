import { Component, OnInit } from '@angular/core';
import { Usuario } from './core/usuario.model';
import { UsuarioService } from './core/usuario.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth/auth.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-cadastrar-usuario',
  templateUrl: './cadastrar-usuario.component.html',
  styleUrls: ['./cadastrar-usuario.component.css']
})
export class CadastrarUsuarioComponent implements OnInit {
  form!: FormGroup;


  constructor(private _usuarioService: UsuarioService, private _fb: FormBuilder, private _authService: AuthService, private router: Router) { }

  ngOnInit() {
    this.formulario();
  }
  onSubmit(): void {
    if (this.form.valid)
      {
        const cpf = this.form.controls['cpf'].value.replace(/[^\d]/g, ''); // Remove caracteres não numéricos
    if (!this.validarCPF(cpf)) {
      this.form.controls['cpf'].setErrors({ cpfInvalido: true });
      return;
    }

      this._usuarioService
        .cadastrarUsuario({
          user: this.form.controls['user'].value,
          password: this.form.controls['password'].value,
          cpf:cpf,
          telefone: this.form.controls['telefone'].value,
        })
        .subscribe((success) => {
          if (success) {
            this._authService.message('Cadastrado com sucesso');
            this.router.navigate(['/login']);
          }
        }, erro => {
          console.log(erro)
          this._authService.message('nao cadastrado');

        });
    }
  }
  formulario() {
    this.form = this._fb.group({
      user: [null, Validators.compose([Validators.required,]),],
      password: [null, Validators.compose([Validators.required, Validators.minLength(4),]),],
      cpf: [null, Validators.compose([Validators.required,]),],
      telefone: [null, Validators.compose([Validators.required,]),],
    });
  }
   validarCPF(cpf: string): boolean {
    cpf = cpf.replace(/[^\d]/g, '');
    if (cpf.length !== 11 || /^(.)\1+$/.test(cpf)) {
      return false;
    }

    let sum = 0;
    for (let i = 0; i < 9; i++) {
      sum += parseInt(cpf.charAt(i)) * (10 - i);
    }

    let remainder = 11 - (sum % 11);
    if (remainder === 10 || remainder === 11) {
      remainder = 0;
    }

    if (remainder !== parseInt(cpf.charAt(9))) {
      return false;
    }

    sum = 0;
    for (let i = 0; i < 10; i++) {
      sum += parseInt(cpf.charAt(i)) * (11 - i);
    }

    remainder = 11 - (sum % 11);
    if (remainder === 10 || remainder === 11) {
      remainder = 0;
    }

    if (remainder !== parseInt(cpf.charAt(10))) {
      return false;
    }

    return true;
  }
}

