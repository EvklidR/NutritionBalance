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

  private currentProfileSubject = new BehaviorSubject<Profile | null>(null);
  public currentProfile$ = this.currentProfileSubject.asObservable();

  private profilesSubject = new BehaviorSubject<Profile[]>([]);
  public profiles$ = this.profilesSubject.asObservable();

  constructor(private http: HttpClient) {
  }

  getUserProfiles(): Observable<Profile[]> {
    return this.http.get<Profile[]>(`${this.apiUrl}/by-user`).pipe(
      tap((profiles) => {
        this.profilesSubject.next(profiles);
      })
    );
  }

  createProfile(profile: CreateProfileDTO): Observable<Profile> {
    return this.http.post<Profile>(`${this.apiUrl}`, profile);
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

  chooseMealPlan(profileId: number, mealPlanId: number): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/choose-meal-plan?profileId=${profileId}&mealPlanId=${mealPlanId}`, {});
  }

  calculateDailyNeeds(profileId: number): Observable<DailyNeedsResponse> {
    return this.http.get<DailyNeedsResponse>(`${this.apiUrl}/${profileId}/daily-needs`);
  }

  revokeMealPlan(profileId: number): Observable<void> {
    return this.http.patch<void>(`${this.apiUrl}/${profileId}/revoke-meal-plan`, {});
  }

  setCurrentProfile(profile: Profile): void {
    localStorage.setItem('currentProfileId', profile.id.toString());
    this.currentProfileSubject.next(profile);
  }

  clearCurrentProfile(): void {
    localStorage.removeItem('currentProfileId');
    this.currentProfileSubject.next(null);
  }

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

  addProfileToList(profile: Profile): void {
    const updatedProfiles = [...this.profilesSubject.getValue(), profile];
    this.profilesSubject.next(updatedProfiles);
  }
}
