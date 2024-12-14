import { Answer } from './answer.model'
export class Question {
  id!: number;
  questionText!: string;
  answers!: Answer[];
}
