import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Meal } from '../../models/profile/entities/meal.model';
import { CreateMealDTO } from '../../models/profile/DTOs/dayResult/create-meal.dto';
import { UpdateMealDTO } from '../../models/profile/DTOs/dayResult/update-meal.dto';
import { EatenFood } from '../../models/profile/entities/eaten-food.model'; // Импорт модели еды

@Injectable({
  providedIn: 'root',
})
export class MealService {
  private apiUrl = `https://localhost:7078/user-profile-service/meal`;

  constructor(private http: HttpClient) { }

  addMeal(meal: CreateMealDTO): Observable<Meal> {
    return this.http.post<Meal>(`${this.apiUrl}`, meal);
  }

  updateMeal(meal: UpdateMealDTO): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}`, meal);
  }

  deleteMeal(mealId: number, dayId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${mealId}/day/${dayId}`);
  }

  calculateTotalNutrients(foods: EatenFood[]): { calories: number, proteins: number, fats: number, carbs: number } {
    return foods.reduce(
      (total, eatenFood) => {
        const foodCalories = eatenFood.food?.calories || 0;
        const foodProteins = eatenFood.food?.proteins || 0;
        const foodFats = eatenFood.food?.fats || 0;
        const foodCarbs = eatenFood.food?.carbohydrates || 0;
        const foodWeight = eatenFood.weight || 0;

        total.calories += (foodCalories * (foodWeight / 100));
        total.proteins += (foodProteins * (foodWeight / 100));
        total.fats += (foodFats * (foodWeight / 100));
        total.carbs += (foodCarbs * (foodWeight / 100));

        return total;
      },
      { calories: 0, proteins: 0, fats: 0, carbs: 0 }
    );
  }

}
