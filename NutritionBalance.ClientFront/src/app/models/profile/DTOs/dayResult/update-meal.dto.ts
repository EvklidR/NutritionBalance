import { CreateOrUpdateEatenFoodDTO } from './create-or-update-eaten-food.dto';

export class UpdateMealDTO {
  id!: number;
  dayResultId!: number;
  name!: string;
  foods: CreateOrUpdateEatenFoodDTO[] = [];
}
