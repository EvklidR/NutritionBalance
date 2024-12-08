import { MealPlanDayDTO } from './meal-plan-day.dto';
import { MealPlanType } from '../enums/meal-plan-type.enum';

export class UpdateMealPlanDTO {
  id!: number;
  name!: string;
  description!: string;
  type!: MealPlanType;
  days: MealPlanDayDTO[] = [];
}
