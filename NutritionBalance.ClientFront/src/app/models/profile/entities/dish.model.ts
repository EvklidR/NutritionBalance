import { IngredientOfDish } from './ingredient-of-dish.model';
import { Food } from './food.model'

export class Dish extends Food {
  description?: string;
  imageUrl?: string;
  weightOfPortion!: number;
  ingredients: IngredientOfDish[] = [];
}
