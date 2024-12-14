import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  email: string = '';
  login: string = '';
  password: string = '';
  confirmPassword: string = '';
  errorMessage: string = '';

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  onSubmit(form: any): void {
    if (!form.valid) {
      return;
    }

    const { email, login, password, confirmPassword } = form.value;

    if (password !== confirmPassword) {
      this.errorMessage = 'Пароли не совпадают.';
      return;
    }

    this.authService.register({ email, login, password }).subscribe({
      next: () => {
        this.errorMessage = '';
        setTimeout(() => this.router.navigate(['/']), 3000);
      },
      error: (error) => {
        this.errorMessage = error.error?.message || 'Ошибка при регистрации, попробуйте снова!';
      }
    });
  }
}
