import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { MealPlanCreateDTO } from '../models/mealPlan/DTOs/meal-plan-create.dto';
import { UpdateMealPlanDTO } from '../models/mealPlan/DTOs/update-meal-plan.dto';
import { MealPlanType } from '../models/mealPlan/enums/meal-plan-type.enum'
import { MealPlan } from '../models/mealPlan/entities/meal-plan.model';
import { Ingredient } from '../models/profile/entities/ingredient.model';

@Injectable({
  providedIn: 'root',
})
export class MealPlanService {
  private baseUrl = 'https://localhost:7078/meal-plan-service/MealPlan';

  constructor(private http: HttpClient) { }

  getMealPlansByCategory(type: MealPlanType, pageNumber: number, pageSize: number): Observable<MealPlan[]> {
    const url = `${this.baseUrl}/by-category`;
    return this.http.get<MealPlan[]>(url, {
      params: { type, pageNumber, pageSize },
    });
  }

  getMealPlansByOwner(): Observable<MealPlan[]> {
    const url = `${this.baseUrl}/by-owner`;
    return this.http.get<MealPlan[]>(url);
  }

  getMealPlansById(id: number): Observable<MealPlan> {
    const url = `${this.baseUrl}/by-id/${id}`;
    return this.http.get<MealPlan>(url);
  }

  createMealPlan(mealPlanData: MealPlanCreateDTO): Observable<MealPlan> {
    const payload = { MealPlanDto: mealPlanData };
    const url = `${this.baseUrl}/create`;
    return this.http.post<MealPlan>(url, payload);
  }

  updateMealPlan(updateData: UpdateMealPlanDTO): Observable<any> {
    const url = `${this.baseUrl}/update`;
    return this.http.put(url, updateData);
  }

  deleteMealPlan(mealPlanId: number): Observable<any> {
    const url = `${this.baseUrl}/delete?id=${mealPlanId}`;
    return this.http.delete(url);
  }

  calculateKcalAndMacros(queryParams: any): Observable<any> {
    const url = `${this.baseUrl}/calculate-kcal`;
    return this.http.get(url, { params: queryParams });
  }

  getFile(fileName: string): Observable<Blob> {
    const url = `${this.baseUrl}/get-file/${fileName}`;
    return this.http.get(url, { responseType: 'blob' });
  }

  updateFile(mealPlanId: number, file: File): Observable<void> {
    const url = `${this.baseUrl}/update-file`;
    const formData = new FormData();
    formData.append('mealPlanId', mealPlanId.toString());
    formData.append('file', file);

    return this.http.patch<void>(url, formData);
  }
}
