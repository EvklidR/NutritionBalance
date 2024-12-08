import { Ingredient } from './ingredient.model';

export class IngredientOfDish {
  dishId!: number;
  ingredientId!: number;
  weight!: number;
  ingredient!: Ingredient;
}
