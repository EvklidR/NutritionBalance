import { MealPlanType } from '../enums/meal-plan-type.enum';

export class MealPlan {
  id!: number;
  ownerId!: number;
  name!: string;
  description!: string;
  type!: MealPlanType;
  imageUrl?: string;
}
