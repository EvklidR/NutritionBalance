import { NutrientOfDayDTO } from './nutrient-of-day.dto';

export class MealPlanDayDTO {
  numberOfDay!: number;
  caloriePercentage: number = 1;
  nutrientsOfDay: NutrientOfDayDTO[] = [];
}
