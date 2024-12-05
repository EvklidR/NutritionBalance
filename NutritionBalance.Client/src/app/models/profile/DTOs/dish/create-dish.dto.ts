import { IngredientOfDishDTO } from './ingredient-of-dish.dto';

export class CreateDishDTO {
  profileId!: number;
  name!: string;
  description?: string;
  amountOfPortions!: number;
  ingredients: IngredientOfDishDTO[] = [];
}
