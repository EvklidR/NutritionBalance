import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create-profile',
  templateUrl: './create-profile.component.html',
  styleUrls: ['./create-profile.component.css']
})
export class CreateProfileComponent {
  profileName: string = '';
  errorMessage: string = '';
  successMessage: string = '';

  constructor(private router: Router) { }

  onSubmit(): void {
    if (!this.profileName.trim()) {
      this.errorMessage = 'Имя профиля не может быть пустым!';
      return;
    }

    console.log(`Создан новый профиль: ${this.profileName}`);
    this.successMessage = 'Профиль успешно создан!';
    this.errorMessage = '';
    setTimeout(() => this.router.navigate(['/profile-selection']), 2000);
  }

  cancel(): void {
    this.router.navigate(['/profile-selection']);
  }
}
