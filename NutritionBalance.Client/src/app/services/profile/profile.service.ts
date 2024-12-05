import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Profile } from '../../models/profile/entities/profile.model';
import { CreateProfileDTO } from '../../models/profile/DTOs/profile/create-profile.dto';
import { UpdateProfileDTO } from '../../models/profile/DTOs/profile/update-profile.dto';

@Injectable({
  providedIn: 'root',
})
export class ProfileService {
  private apiUrl = `https://localhost:7078/user-profile-service/profile`;

  constructor(private http: HttpClient) { }

  createProfile(profile: CreateProfileDTO): Observable<Profile> {
    return this.http.post<Profile>(`${this.apiUrl}`, profile);
  }

  calculateDailyNeeds(profileId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/${profileId}/daily-needs`);
  }

  chooseMealPlan(profileId: number, mealPlanId: number): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/choose-meal-plan`, null, {
      params: { profileId, mealPlanId },
    });
  }

  revokeMealPlan(profileId: number): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${profileId}/revoke-meal-plan`, null);
  }

  getUserProfiles(): Observable<Profile[]> {
    return this.http.get<Profile[]>(`${this.apiUrl}/by-user`);
  }

  updateProfile(profile: UpdateProfileDTO): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${profile.id}`, profile);
  }

  deleteProfile(profileId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${profileId}`);
  }

  getUserById(profileId: number): Observable<Profile> {
    return this.http.get<Profile>(`${this.apiUrl}/by-id/${profileId}`);
  }
}
