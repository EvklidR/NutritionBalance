import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CreateUserDto } from '../models/auth/DTOs/create-user.dto';
import { map, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import jwt_decode from 'jwt-decode';

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
    localStorage.removeItem('currentProfileId');
    this.router.navigate(['/login']);
  }

  //isAdmin(): boolean {
  //  const token = this.getAccessToken();
  //  if (!token) {
  //    return false;
  //  }

  //  try {
  //    const decodedToken: any = jwt_decode(token);
  //    return decodedToken.role === 'admin';
  //  } catch (error) {
  //    console.error('Error decoding token', error);
  //    return false;
  //  }
  //}

}
