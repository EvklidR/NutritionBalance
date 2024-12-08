import { IngredientOfDishDTO } from './ingredient-of-dish.dto';

export class UpdateDishDTO {
  id!: number;
  name!: string;
  description?: string;
  amountOfPortions: number = 0;
  ingredients: IngredientOfDishDTO[] = [];
}
