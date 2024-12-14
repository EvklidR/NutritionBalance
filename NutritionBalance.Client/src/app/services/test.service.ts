import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Question } from '../models/test/question.model'

@Injectable({
  providedIn: 'root',
})
export class TestService {
  private baseUrl = 'https://localhost:7078/test-service';

  constructor(private http: HttpClient) { }

  evaluateAnswers(answers: number[]): Observable<number> {
    const url = `${this.baseUrl}/evaluate`;
    return this.http.post<number>(url, answers);
  }

  getQustions(): Observable<Question[]> {
    const url = `${this.baseUrl}/questions`;
    return this.http.get<Question[]>(url);
  }
}
