import { Meal } from './meal.model'

export class DayResult {
  id!: number;
  profileId!: number;
  date!: string;
  weight?: number;
  height?: number;
  activityLevel?: number;
  glassesOfWater: number = 0;
  meals: Meal[] = [];
}
