import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { UpdateDayResultDTO } from '../../models/profile/DTOs/dayResult/update-day-result.dto';
import { CreateDayResultDTO } from '../../models/profile/DTOs/dayResult/create-day-result.dto'
import { DayResult } from '../../models/profile/entities/day-result.model'

@Injectable({
  providedIn: 'root',
})
export class DayResultService {
  private apiUrl = `https:/localhost:7078/user-profile-service/dayResult`;

  constructor(private http: HttpClient) { }

  getDayResultById(id: number): Observable<DayResult> {
    return this.http.get<DayResult>(`${this.apiUrl}/${id}`);
  }

  getDayResultsByPeriod(profileId: number, startDate: string, endDate: string): Observable<DayResult[]> {
    const params = new HttpParams()
      .set('profileId', profileId.toString())
      .set('startDate', startDate)
      .set('endDate', endDate);

    return this.http.get<DayResult[]>(`${this.apiUrl}/by-period`, { params });
  }

  createDayResult(dto: CreateDayResultDTO): Observable<DayResult> {
    return this.http.post<DayResult>(this.apiUrl, dto);
  }

  updateDayResult(dto: UpdateDayResultDTO): Observable<void> {
    return this.http.put<void>(this.apiUrl, dto);
  }

  deleteDayResult(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  getOrCreateDayResult(profileId: number, date: string): Observable<DayResult> {
    const params = new HttpParams()
      .set('profileId', profileId.toString())
      .set('date', date);

    return this.http.get<DayResult>(`${this.apiUrl}/get-or-create`, { params });
  }
}
