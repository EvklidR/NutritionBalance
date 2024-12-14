import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Dish } from '../../models/profile/entities/dish.model';
import { CreateDishDTO } from '../../models/profile/DTOs/dish/create-dish.dto'
import { UpdateDishDTO } from '../../models/profile/DTOs/dish/update-dish.dto'

@Injectable({
  providedIn: 'root',
})
export class DishService {
  private apiUrl = `https:/localhost:7078/user-profile-service/dish`;

  constructor(private http: HttpClient) { }

  updateImage(dishId: number, file: File): Observable<void> {
    const formData = new FormData();
    formData.append('dishId', dishId.toString());
    formData.append('file', file, file.name);

    return this.http.patch<void>(`${this.apiUrl}/update-file`, formData);
  }

  getImage(fileName: string): Observable<Blob> {
    const url = `${this.apiUrl}/get-file/${fileName}`;
    return this.http.get(url, { responseType: 'blob' });
  }

  getDishById(id: number): Observable<Dish> {
    return this.http.get<Dish>(`${this.apiUrl}/${id}`);
  }

  getAllDishes(profileId: number): Observable<Dish[]> {
    return this.http.get<Dish[]>(`${this.apiUrl}/profile/${profileId}`);
  }

  createDish(dish: CreateDishDTO): Observable<Dish> {
    return this.http.post<Dish>(`${this.apiUrl}`, dish);
  }

  updateDish(dish: UpdateDishDTO): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}`, dish);
  }

  deleteDish(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
