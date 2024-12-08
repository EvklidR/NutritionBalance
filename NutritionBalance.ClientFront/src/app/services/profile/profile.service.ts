import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { tap } from 'rxjs/operators';
import { Profile } from '../../models/profile/entities/profile.model';
import { CreateProfileDTO } from '../../models/profile/DTOs/profile/create-profile.dto';
import { UpdateProfileDTO } from '../../models/profile/DTOs/profile/update-profile.dto';
import { DailyNeedsResponse } from '../../models/profile/DTOs/profile/daily-needs-response.dto';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private apiUrl = `https://localhost:7078/user-profile-service/profile`;

  // Это для текущего профиля
  private currentProfileSubject = new BehaviorSubject<Profile | null>(null);
  public currentProfile$ = this.currentProfileSubject.asObservable();

  // Теперь это BehaviorSubject для списка профилей
  private profilesSubject = new BehaviorSubject<Profile[]>([]);
  public profiles$ = this.profilesSubject.asObservable();  // Публичный поток для подписчиков

  constructor(private http: HttpClient) {
    this.loadCurrentProfile();
  }

  // Метод для получения всех профилей с сервера
  getUserProfiles(): Observable<Profile[]> {
    return this.http.get<Profile[]>(`${this.apiUrl}/by-user`).pipe(
      tap((profiles) => {
        this.profilesSubject.next(profiles);  // Обновляем список профилей через BehaviorSubject
      })
    );
  }

  // Метод для создания нового профиля
  createProfile(profile: CreateProfileDTO): Observable<Profile> {
    return this.http.post<Profile>(`${this.apiUrl}`, profile);
  }

  // Метод для обновления профиля
  updateProfile(profile: UpdateProfileDTO): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${profile.id}`, profile);
  }

  // Метод для удаления профиля
  deleteProfile(profileId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${profileId}`);
  }

  // Метод для получения профиля по ID
  getUserById(profileId: number): Observable<Profile> {
    return this.http.get<Profile>(`${this.apiUrl}/by-id/${profileId}`);
  }

  // Метод для выбора плана питания для профиля
  chooseMealPlan(profileId: number, mealPlanId: number): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/choose-meal-plan?profileId=${profileId}&mealPlanId=${mealPlanId}`, {});
  }

  // Метод для расчета дневных потребностей по профилю
  calculateDailyNeeds(profileId: number): Observable<DailyNeedsResponse> {
    return this.http.get<DailyNeedsResponse>(`${this.apiUrl}/${profileId}/daily-needs`);
  }

  // Метод для отмены выбранного плана питания
  revokeMealPlan(profileId: number): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${profileId}/revoke-meal-plan`, {});
  }

  // Метод для установки текущего профиля
  setCurrentProfile(profile: Profile): void {
    localStorage.setItem('currentProfileId', profile.id.toString());
    this.currentProfileSubject.next(profile);
  }

  // Метод для очистки текущего профиля
  clearCurrentProfile(): void {
    localStorage.removeItem('currentProfileId');
    this.currentProfileSubject.next(null);
  }

  // Загрузка текущего профиля из localStorage
  loadCurrentProfile(): void {
    const profileId = localStorage.getItem('currentProfileId');
    if (profileId) {
      const profile = this.profilesSubject.getValue().find(p => p.id === parseInt(profileId, 10));
      if (profile) {
        this.currentProfileSubject.next(profile);
      } else {
        this.currentProfileSubject.next(null);
      }
    } else {
      this.currentProfileSubject.next(null);
    }
  }

  // Метод для добавления нового профиля в список
  addProfileToList(profile: Profile): void {
    const updatedProfiles = [...this.profilesSubject.getValue(), profile]; // Добавляем новый профиль в список
    this.profilesSubject.next(updatedProfiles); // Обновляем список
  }
}
