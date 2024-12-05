import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Meal } from '../../models/profile/entities/meal.model';
import { CreateMealDTO } from '../../models/profile/DTOs/dayResult/create-meal.dto'
import { UpdateMealDTO } from '../../models/profile/DTOs/dayResult/update-meal.dto'

@Injectable({
  providedIn: 'root',
})
export class MealService {
  private apiUrl = `https://localhost:7078//user-profile-service//meal`;

  constructor(private http: HttpClient) { }

  addMeal(meal: CreateMealDTO): Observable<void> {
    return this.http.post<void>(`${this.apiUrl}`, meal);
  }

  updateMeal(meal: UpdateMealDTO): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}`, meal);
  }

  deleteMeal(mealId: number, dayId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${mealId}/day/${dayId}`);
  }
}
