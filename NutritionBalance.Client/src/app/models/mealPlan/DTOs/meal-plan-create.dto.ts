import { MealPlanDayDTO } from './meal-plan-day.dto';
import { MealPlanType } from '../enums/meal-plan-type.enum';

export class MealPlanCreateDTO {
  ownerId!: number;
  name!: string;
  description!: string;
  type!: MealPlanType;
  days: MealPlanDayDTO[] = [];
}
