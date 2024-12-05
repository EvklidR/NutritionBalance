import { Gender } from '../enums/gender.enum';
import { DayResult } from './day-result.model'

export class Profile {
  id!: number;
  userId!: number;
  name!: string;
  weight!: number;
  height!: number;
  birthday!: string;
  gender!: Gender;
  activityLevel!: number;
  desiredGlassesOfWater!: number;
  mealPlanId?: number;
  dateOfStartPlan?: string;
  dayResults: DayResult[] = [];
}
