import { Component, OnInit } from '@angular/core';
import { TestService } from '../../services/test.service';
import { Question } from '../../models/test/question.model';

@Component({
  selector: 'app-test-page',
  templateUrl: './test-page.component.html',
  styleUrls: ['./test-page.component.css']
})
export class TestPageComponent implements OnInit {
  questions: Question[] = [];
  selectedAnswers: number[] = [];
  evaluationResult: number = 0;
  openModal: boolean = false;

  constructor(private testService: TestService) { }

  ngOnInit(): void {
    this.loadQuestions();
  }

  loadQuestions(): void {
    this.testService.getQustions().subscribe({
      next: (data) => {
        this.questions = data;
      },
      error: (err) => {
        console.error('Failed to load questions', err);
      }
    });
  }

  toggleAnswer(answerId: number): void {
    if (answerId in this.selectedAnswers) {
      this.selectedAnswers.filter(id => id !== answerId);
    } else {
      this.selectedAnswers.push(answerId);
    }
  }

  submitAnswers(): void {
    this.testService.evaluateAnswers(this.selectedAnswers).subscribe({
      next: (result) => {
        this.evaluationResult = result;
        this.openModal = true;
        console.log(this.evaluationResult)
      },
      error: (err) => {
        console.error('Failed to submit answers', err);
      }
    });
  }

  closeModal() {
    this.openModal = false;
  }
}
