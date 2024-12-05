import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MealPlanCreateDTO } from '../models/mealPlan/DTOs/meal-plan-create.dto';
import { UpdateMealPlanDTO } from '../models/mealPlan/DTOs/update-meal-plan.dto';

@Injectable({
  providedIn: 'root',
})
export class MealPlanService {
  private baseUrl = 'https://localhost:7078/meal-plan-service';

  constructor(private http: HttpClient) { }

  getMealPlansByCategory(type: string, pageNumber: number, pageSize: number): Observable<any> {
    const url = `${this.baseUrl}/meal-plan/by-category`;
    return this.http.get(url, {
      params: { type, pageNumber, pageSize },
    });
  }

  getMealPlansByOwner(): Observable<any> {
    const url = `${this.baseUrl}/meal-plan/by-owner`;
    return this.http.get(url);
  }

  createMealPlan(mealPlanData: MealPlanCreateDTO): Observable<any> {
    const url = `${this.baseUrl}/meal-plan/create`;
    return this.http.post(url, mealPlanData);
  }

  updateMealPlan(updateData: UpdateMealPlanDTO): Observable<any> {
    const url = `${this.baseUrl}/meal-plan/update`;
    return this.http.put(url, updateData);
  }

  deleteMealPlan(mealPlanId: number): Observable<any> {
    const url = `${this.baseUrl}/meal-plan/delete?id=${mealPlanId}`;
    return this.http.delete(url);
  }

  calculateKcalAndMacros(queryParams: any): Observable<any> {
    const url = `${this.baseUrl}/meal-plan/calculate-kcal`;
    return this.http.get(url, { params: queryParams });
  }
}
