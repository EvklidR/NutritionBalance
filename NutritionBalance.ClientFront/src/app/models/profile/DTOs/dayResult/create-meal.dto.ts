import { CreateOrUpdateEatenFoodDTO } from './create-or-update-eaten-food.dto';

export class CreateMealDTO {
  dayResultId!: number;
  name!: string;
  foods: CreateOrUpdateEatenFoodDTO[] = [];
}
