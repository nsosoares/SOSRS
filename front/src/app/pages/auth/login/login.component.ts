import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../auth.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styles: [
        `
          mat-card {
            max-width: 400px;
            margin: 2em auto;
            text-align: center;
          }

          mat-form-field {
            display: block;
          }
        `,
    ],
})
export class LoginComponent {
    valCheck: string[] = ['remember'];
    password!: string;
    public form!: FormGroup;

    constructor(
        private formBuilder: FormBuilder,
        private router: Router,
        private authService: AuthService
    ) {
    }

    onSubmit(): void {
        if (this.form.valid) {
            this.authService
                .login({
                    user: this.form.controls['email'].value,
                    password: this.form.controls['password'].value,
                })
                .subscribe((success) => {
                    if (success) {
                        this.authService.message('Login com relizado com sucesso');
                        this.router.navigate(['/']);
                    }
                });
        }
    }

    ngOnInit() {
        this.createForm();
    }

    createForm() {
        this.form = this.formBuilder.group({
            email: [null, Validators.compose([Validators.required, ]),],
            password: [null, Validators.compose([Validators.required,Validators.minLength(4),]),
            ],
        });
    }
}
