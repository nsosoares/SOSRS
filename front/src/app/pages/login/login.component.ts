import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'cw-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  loginForm = new FormGroup({
    username: new FormControl('', Validators.required), // Add required validator
    password: new FormControl('', Validators.required), // Add required validator
  });

  onLogin() {
    if (this.loginForm.valid) { // Check if form is valid
      console.log(this.loginForm.value);
    } else {
      console.log('Invalid form');
    }
  }
}
