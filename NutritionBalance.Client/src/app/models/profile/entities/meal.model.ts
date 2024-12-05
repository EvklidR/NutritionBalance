import { EatenFood } from './eaten-food.model';

export class Meal {
  id!: number;
  dayId!: number;
  name!: string;
  foods: EatenFood[] = [];
}
