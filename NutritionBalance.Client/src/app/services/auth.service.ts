import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CreateUserDto } from '../models/auth/DTOs/create-user.dto';
import { map, tap } from 'rxjs/operators';
import { Observable, BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

import { decode } from 'base-64';
window.atob = decode;

import { throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly baseUrl: string = 'https://localhost:7078/authorisation-service';

  constructor(private http: HttpClient, private router: Router) {}


  login(command: { username: string, password: string }): Observable<any> {
    return this.http.post(`${this.baseUrl}/auth/login`, command).pipe(
      map((response: any) => {
        if (response.accessToken && response.refreshToken) {
          this.setAccessToken(response.accessToken);
          this.setRefreshToken(response.refreshToken);
          const role = this.getUserRoleFromToken()
          if (role != null) {
            this.setUserRole(role)
          }
        }
        return response;
      })
    );
  }

  register(command: CreateUserDto): Observable<any> {
    const payload = { CreateUserDto: command };
    return this.http.post(`${this.baseUrl}/auth/register`, payload).pipe(
      map((response: any) => {
        if (response?.accessToken && response?.refreshToken) {
          this.setAccessToken(response.accessToken);
          this.setRefreshToken(response.refreshToken);
        }
        return response;
      })
    );
  }

  refreshToken(): Observable<any> {
    const refreshToken = this.getRefreshToken();
    const accessToken = this.getAccessToken();

    return this.http.post(`${this.baseUrl}/token/refresh`, {
      refreshToken,
      accessToken
    }).pipe(
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
      tap(() => this.removeTokens())
    );
  }

  logout(): void {
    this.revokeToken()
    this.removeTokens()
    this.router.navigate(['/login']);
  }

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

  private getUserRole(): string | null {
    return localStorage.getItem('role');
  }

  private setUserRole(token: string): void {
    localStorage.setItem('role', token);
  }

  private removeTokens(): void {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
    localStorage.removeItem('role');
    localStorage.removeItem('currentProfileId');
  }

  isAuthenticated(): boolean {
    const token = localStorage.getItem('accessToken');
    return !!token;
  }

  isAdmin(): boolean {
    return this.getUserRole() == "admin";
  }

  private getUserRoleFromToken(): string | null {
    const token = localStorage.getItem('accessToken');

    if (!token) {
      this.logout();
      return null;
    }

    const payload = token.split('.')[1];
    if (!payload) {
      this.logout();
      return null;
    }

    try {
      const decodedPayload = atob(payload);
      const payloadObject = JSON.parse(decodedPayload);
      const role = payloadObject['role'] || payloadObject['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] || null;

      if (!role) {
        this.logout();
        return null;
      }

      return role;
    } catch (error) {
      console.error('Ошибка при декодировании токена:', error);
      this.logout();
      return null;
    }
  }
}
