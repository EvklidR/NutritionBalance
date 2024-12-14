import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';
  errorMessage: string = '';
  formSubmitted: boolean = false;

  constructor(private authService: AuthService, private router: Router) { }

  get usernameInvalid(): boolean {
    return !this.username;
  }

  get passwordInvalid(): boolean {
    return !this.password;
  }

  onSubmit(): void {
    this.formSubmitted = true;

    if (this.usernameInvalid || this.passwordInvalid) {
      this.errorMessage = 'Пожалуйста, заполните все поля.';
      return;
    }

    this.authService.login({ username: this.username, password: this.password }).subscribe({
      next: () => {
        this.router.navigate(['/']);
      },
      error: () => {
        this.errorMessage = 'Неверное имя пользователя или пароль!';
      }
    });
  }
}
