import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { ProfileService } from '../../services/profile/profile.service';
import { CreateProfileDTO } from '../../models/profile/DTOs/profile/create-profile.dto';
import { Gender } from '../../models/profile/enums/gender.enum';
import { ActivityLevel } from '../../models/profile/enums/activity-level.enum';

@Component({
    selector: 'app-create-profile',
    templateUrl: './create-profile.component.html',
    styleUrls: ['./create-profile.component.css'],
    standalone: false
})
export class CreateProfileComponent {
  profileName: string = '';
  weight: number = 0;
  height: number = 0;
  birthday: Date = new Date();
  gender: Gender = Gender.Male;
  activityLevel: ActivityLevel = ActivityLevel.Low;

  genderEnum = Gender;
  activityLevelEnum = ActivityLevel;

  errorMessage: string = '';
  successMessage: string = '';

  constructor(
    private router: Router,
    private profileService: ProfileService
  ) { }

  onSubmit(): void {
    if (!this.profileName.trim()) {
      this.errorMessage = 'Имя профиля не может быть пустым!';
      return;
    }

    const newProfile: CreateProfileDTO = {
      name: this.profileName,
      weight: this.weight,
      height: this.height,
      birthday: this.birthday,
      gender: Number(this.gender),
      activityLevel: Number(this.activityLevel)
    };

    this.profileService.createProfile(newProfile).subscribe({
      next: (profile) => {
        console.log('Создан новый профиль:', profile);

        this.profileService.addProfileToList(profile);
        this.profileService.setCurrentProfile(profile);

        this.successMessage = 'Профиль успешно создан!';
        this.errorMessage = '';

        this.router.navigate(['/home']);
      },
      error: (err) => {
        console.error('Ошибка при создании профиля:', err);
        this.errorMessage = 'Произошла ошибка при создании профиля!';
        this.successMessage = '';
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/profile-selection']);
  }
}
