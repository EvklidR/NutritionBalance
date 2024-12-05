import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  login: string = '';
  password: string = '';
  email: string = '';
  confirmPassword: string = '';
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private authService: AuthService, private router: Router) { }

  onSubmit(): void {
    if (this.password !== this.confirmPassword) {
      this.errorMessage = 'Пароли не совпадают!';
      return;
    }

    this.authService.register({ email: this.email, login: this.login, password: this.password }).subscribe({
      next: () => {
        this.successMessage = 'Регистрация успешна! Пожалуйста, войдите.';
        this.errorMessage = '';
      },
      error: (error) => {
        this.errorMessage = 'Ошибка при регистрации, попробуйте снова!';
        this.successMessage = '';
      }
    });
  }
}
