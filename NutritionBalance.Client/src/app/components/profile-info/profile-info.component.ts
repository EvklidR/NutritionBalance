import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { ProfileService } from '../../services/profile/profile.service';
import { Profile } from '../../models/profile/entities/profile.model';
import { Subscription } from 'rxjs';
import { ActivityLevel } from '../../models/profile/enums/activity-level.enum';

@Component({
  selector: 'app-profile-info',
  templateUrl: './profile-info.component.html',
  styleUrls: ['./profile-info.component.css']
})
export class ProfileInfoComponent implements OnInit {
  profile!: Profile | null;
  private profileSubscription!: Subscription;

  // Эти данные можно редактировать
  currentWeight: number = 0;
  currentHeight: number = 0;
  activityLevel: ActivityLevel = ActivityLevel.Low;  // По умолчанию значение Low

  // ИМТ
  bmi: number = 0;

  activityLevelOptions: { label: string, value: ActivityLevel }[] = [
    { label: 'Седентарный (малоподвижный)', value: ActivityLevel.Sedentary },
    { label: 'Низкий', value: ActivityLevel.Low },
    { label: 'Средний', value: ActivityLevel.Medium },
    { label: 'Высокий', value: ActivityLevel.High },
    { label: 'Очень высокий', value: ActivityLevel.VeryHigh },
  ];

  constructor(
    private authService: AuthService,
    private router: Router,
    private profileService: ProfileService,
  ) { }

  ngOnInit(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }

    this.profileSubscription = this.profileService.currentProfile$.subscribe((profile) => {
      this.profile = profile;

      if (profile) {
        this.currentWeight = profile.weight || 0;
        this.currentHeight = profile.height || 0;

        // Преобразуем числовое значение активности в ActivityLevel
        this.activityLevel = this.getActivityLevelFromNumber(profile.activityLevel || 1.375);
        this.calculateBMI();
      }
    });
  }

  // Метод для расчета ИМТ (Индекс массы тела)
  calculateBMI(): void {
    if (this.currentHeight > 0 && this.currentWeight > 0) {
      this.bmi = this.currentWeight / ((this.currentHeight / 100) ** 2);  // ИМТ = вес (кг) / (рост (м))^2
    }
  }

  // Метод для обновления данных
  updateProfile(): void {
    if (this.profile) {
      this.profile.weight = this.currentWeight;
      this.profile.height = this.currentHeight;
      this.profile.activityLevel = this.activityLevel;

      // Здесь вы могли бы вызвать метод сервиса, чтобы обновить данные на сервере
      this.profileService.updateProfile(this.profile).subscribe((updatedProfile) => {
        console.log("Обновлено");
        this.calculateBMI(); // Пересчитываем ИМТ
      });
    }
  }

  // Метод для получения строки для уровня активности
  getActivityLevelString(value: ActivityLevel): string {
    switch (value) {
      case ActivityLevel.Sedentary: return 'Седентарный (малоподвижный)';
      case ActivityLevel.Low: return 'Низкий';
      case ActivityLevel.Medium: return 'Средний';
      case ActivityLevel.High: return 'Высокий';
      case ActivityLevel.VeryHigh: return 'Очень высокий';
      default: return '';
    }
  }

  // Метод для преобразования числа в ActivityLevel
  getActivityLevelFromNumber(value: number): ActivityLevel {
    switch (value) {
      case 1.2: return ActivityLevel.Sedentary;
      case 1.375: return ActivityLevel.Low;
      case 1.55: return ActivityLevel.Medium;
      case 1.725: return ActivityLevel.High;
      case 1.9: return ActivityLevel.VeryHigh;
      default: return ActivityLevel.Low;  // По умолчанию низкий уровень
    }
  }
}
