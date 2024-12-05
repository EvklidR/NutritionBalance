import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ingredient } from '../../models/profile/entities/ingredient.model';
import { CreateIngredientDTO } from '../../models/profile/DTOs/ingredient/create-ingredient.dto'
import { UpdateIngredientDTO } from '../../models/profile/DTOs/ingredient/update-ingredient.dto'

@Injectable({
  providedIn: 'root',
})
export class IngredientService {
  private apiUrl = `https://localhost:7078//user-profile-service//ingredient`;

  constructor(private http: HttpClient) { }

  getIngredients(profileId: number): Observable<Ingredient[]> {
    return this.http.get<Ingredient[]>(`${this.apiUrl}/profile/${profileId}`);
  }

  getIngredientsFromApi(name: string): Observable<Ingredient[]> {
    return this.http.get<Ingredient[]>(`${this.apiUrl}/from-api`, {
      params: { name },
    });
  }

  createIngredient(ingredient: CreateIngredientDTO): Observable<Ingredient> {
    return this.http.post<Ingredient>(`${this.apiUrl}`, ingredient);
  }

  updateIngredient(ingredient: UpdateIngredientDTO): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}`, ingredient);
  }

  deleteIngredient(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
