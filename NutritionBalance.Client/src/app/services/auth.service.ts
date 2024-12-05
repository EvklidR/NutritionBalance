import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateUserDto } from '../models/auth/DTOs/create-user.dto';
import { map, tap } from 'rxjs/operators';
import { Router } from '@angular/router';

import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly baseUrl: string = 'https://localhost:7078/authorisation-service';

  constructor(private http: HttpClient, private router: Router) { }

  private getAccessToken(): string | null {
    return localStorage.getItem('accessToken');
  }

  private getRefreshToken(): string | null {
    return localStorage.getItem('refreshToken');
  }

  private setAccessToken(token: string): void {
    localStorage.setItem('accessToken', token);
  }

  private setRefreshToken(token: string): void {
    localStorage.setItem('refreshToken', token);
  }

  private removeTokens(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  }

  logout(): void {
    this.revokeToken().subscribe({
      next: () => {
        this.router.navigate(['/login']); // Перенаправление после успешного отзыва токена
      },
      error: (error) => {
        console.error('Ошибка при попытке отозвать токен:', error);
        this.router.navigate(['/login']); // Перенаправление даже при ошибке
      },
      complete: () => {
        this.removeTokens(); // Удаляем токены из локального хранилища
      }
    });
  }


  isAuthenticated(): boolean {
    const token = localStorage.getItem('accessToken');
    return !!token;
  }

  login(command: { username: string, password: string }): Observable<any> {
    return this.http.post(`${this.baseUrl}/auth/login`, command).pipe(
      map((response: any) => {
        if (response.accessToken && response.refreshToken) {
          this.setAccessToken(response.accessToken);
          this.setRefreshToken(response.refreshToken);
        }
        return response;
      })
    );
  }

  register(command: CreateUserDto): Observable<any> {
    const payload = { CreateUserDto: command };
    return this.http.post(`${this.baseUrl}/auth/register`, payload);
  }


  checkUserById(id: number): Observable<boolean> {
    return this.http.get<boolean>(`${this.baseUrl}/auth/check_user_by_id/${id}`);
  }

  refreshToken(): Observable<any> {
    const refreshToken = this.getRefreshToken();
    if (!refreshToken) {
      throw new Error('Refresh token is missing');
    }

    return this.http.post(`${this.baseUrl}/token/refresh`, { refreshToken }).pipe(
      map((response: any) => {
        if (response.accessToken && response.refreshToken) {
          this.setAccessToken(response.accessToken);
          this.setRefreshToken(response.refreshToken);
        }
        return response;
      })
    );
  }

  revokeToken(): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/token/revoke`, {}).pipe(
      tap(() => this.removeTokens()),
      catchError((error) => {
        console.error('Ошибка при попытке отозвать токен:', error);
        return throwError(() => new Error('Не удалось отозвать токен. Попробуйте снова.'));
      })
    );
  }
}
