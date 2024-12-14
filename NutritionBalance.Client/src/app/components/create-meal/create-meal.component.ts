import { Component, EventEmitter, Output, Input, OnInit } from '@angular/core';
import { Meal } from '../../models/profile/entities/meal.model'
import { Ingredient } from '../../models/profile/entities/ingredient.model'
import { Dish } from '../../models/profile/entities/dish.model'
import { IngredientService } from '../../services/profile/ingredient.service'
import { DishService } from '../../services/profile/dish.service'
import { CreateMealDTO } from '../../models/profile/DTOs/dayResult/create-meal.dto'
import { DayResult } from '../../models/profile/entities/day-result.model';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MealService } from '../../services/profile/meal.service'

@Component({
  selector: 'app-create-meal',
  templateUrl: './create-meal.component.html',
  styleUrl: './create-meal.component.css'
})
export class CreateMealComponent implements OnInit {
  @Input() dayResultId: number = 0;
  @Input() profileId: number = 0;
  
  @Output() mealAdded = new EventEmitter<Meal>();
  @Output() closeModal = new EventEmitter<void>();

  constructor(
    private dishService: DishService,
    private ingredientService: IngredientService,
    private snackBar: MatSnackBar,
    private mealService: MealService
  ) { }

  isDropdownVisible = false;
  isDishDropdownVisible = false;

  selectedIngredients: { ingredient: Ingredient, weight: number }[] = [];
  selectedDishes: { dish: Dish, servings: number }[] = [];

  ingredients: Ingredient[] = [];
  dishes: Dish[] = [];

  mealToCreate: CreateMealDTO = new CreateMealDTO();

  ngOnInit() {
    this.loadIngredients();
    this.loadDishes();
    this.selectedDishes = []
    this.selectedIngredients = []
    this.mealToCreate = new CreateMealDTO();
  }

  loadIngredients() {
    this.ingredientService.getIngredients(this.profileId).subscribe(
      (ingredients) => {
        this.ingredients = ingredients;
      },
      (error) => {
        console.error('Ошибка при загрузке ингредиентов', error);
      }
    );
  }

  loadDishes() {
    this.dishService.getAllDishes(this.profileId).subscribe(
      (dishes) => {
        this.dishes = dishes;
      },
      (error) => {
        console.error('Ошибка при загрузке блюд', error);
      }
    );
  }

  showIngredientDropdown() {
    this.isDropdownVisible = true;
  }

  addIngredient(ingredient: Ingredient) {
    this.selectedIngredients.push({ ingredient, weight: 100 });
    this.isDropdownVisible = false;
  }

  removeIngredient(ingredient: Ingredient) {
    this.selectedIngredients = this.selectedIngredients.filter(item => item.ingredient !== ingredient);
  }

  showDishDropdown() {
    this.isDishDropdownVisible = true;
  }

  addDish(dish: Dish) {
    this.selectedDishes.push({ dish, servings: 1 });
    this.isDishDropdownVisible = false;
  }

  removeDish(dish: Dish) {
    this.selectedDishes = this.selectedDishes.filter(item => item.dish !== dish);
  }

  addMeal() {
      if (this.selectedIngredients.length === 0 && this.selectedDishes.length === 0) {
        this.close();
        return;
      }

      if (!this.mealToCreate.name || this.mealToCreate.name.trim() === '') {
        this.snackBar.open('Meal name cannot be empty!', 'Close', {
          duration: 3000,
          verticalPosition: 'bottom',
          horizontalPosition: 'center',
          panelClass: ['snackbar-error']
        });
        return;
      }

      for (const prod of this.selectedDishes) {
        this.mealToCreate.foods.push({ foodId: prod.dish.id, weight: prod.servings * prod.dish.weightOfPortion })
      }
      for (const prod of this.selectedIngredients) {
        this.mealToCreate.foods.push({ foodId: prod.ingredient.id, weight: prod.weight })
      }

      this.mealToCreate.dayResultId = this.dayResultId;

      this.mealService.addMeal(this.mealToCreate).subscribe(
        (meal) => {
          console.log("meal was created", meal)
          this.mealAdded.emit(meal)
          this.close()
        },
        (error) => {
          console.error("Error while creating meal:", error)
        }
      )
  }

  close(): void {
    this.closeModal.emit();
  }
}
