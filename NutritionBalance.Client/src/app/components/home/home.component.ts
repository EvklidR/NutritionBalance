import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { DayResultService } from '../../services/profile/day-result.service';
import { ProfileService } from '../../services/profile/profile.service';
import { MealService } from '../../services/profile/meal.service';
import { DayResult } from '../../models/profile/entities/day-result.model';
import { Profile } from '../../models/profile/entities/profile.model';
import { DailyNeedsResponse } from '../../models/profile/DTOs/profile/daily-needs-response.dto'
import { CreateMealDTO } from '../../models/profile/DTOs/dayResult/create-meal.dto'
import { UpdateDayResultDTO } from '../../models/profile/DTOs/dayResult/update-day-result.dto'
import { UpdateProfileDTO } from '../../models/profile/DTOs/profile/update-profile.dto'
import { Ingredient } from '../../models/profile/entities/ingredient.model'
import { Dish } from '../../models/profile/entities/dish.model'
import { IngredientService } from '../../services/profile/ingredient.service'
import { DishService } from '../../services/profile/dish.service'
import { MatSnackBar } from '@angular/material/snack-bar';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { MatDialog } from '@angular/material/dialog';
import { MealDetailsModalComponent } from '../meal-details-modal/meal-details-modal.component';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  faTrash = faTrash;

  currentUserId!: number;
  dayResult!: DayResult | null;
  profile!: Profile | null;
  dailyNeeads: DailyNeedsResponse = new DailyNeedsResponse();
  totalNutrients = {
    calories: 0,
    proteins: 0,
    fats: 0,
    carbohydrates: 0
  };

  private profileSubscription!: Subscription;
  private dayResultSubscription!: Subscription;

  constructor(
    private authService: AuthService,
    private router: Router,
    private dayResultService: DayResultService,
    private profileService: ProfileService,
    private mealService: MealService,
    private ingredientService: IngredientService,
    private dishService: DishService,
    private snackBar: MatSnackBar,
    private dialog: MatDialog
  ) { }

  ngOnInit(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }

    this.profileSubscription = this.profileService.currentProfile$.subscribe((profile) => {
      this.profile = profile;
      if (profile) {
        this.currentUserId = profile.id;
        this.profileService.calculateDailyNeeds(profile.id).subscribe((dailyNeeads) => {
          console.log(dailyNeeads)
          this.dailyNeeads = dailyNeeads;
          this.getOrCreateDayResult();
        });
      }
    });
  }

  getOrCreateDayResult(): void {
    const today = new Date().toISOString().split('T')[0];
    //const today = new Date().toLocaleDateString('en-CA');

    this.dayResultSubscription = this.dayResultService.getOrCreateDayResult(this.currentUserId, today).subscribe({
      next: (result) => {
        this.dayResult = result;

        let totalCalories = 0;
        let totalProteins = 0;
        let totalFats = 0;
        let totalCarbs = 0;

        if (this.dayResult.meals) {
          this.dayResult.meals.forEach((meal) => {
            const mealNutrients = this.mealService.calculateTotalNutrients(meal.foods);
            totalCalories += mealNutrients.calories;
            totalProteins += mealNutrients.proteins;
            totalFats += mealNutrients.fats;
            totalCarbs += mealNutrients.carbs;
          });
        }

        this.totalNutrients.calories = totalCalories
        this.totalNutrients.proteins = totalProteins
        this.totalNutrients.carbohydrates = totalCarbs
        this.totalNutrients.fats = totalFats
      },

      error: (err) => {
        console.error('Ошибка при получении или создании дневного результата:', err);
        this.dayResult = null;
      },
    });
  }

  getMealNutrients(meal: any): { calories: number, proteins: number, fats: number, carbs: number } {
    if (meal.foods) {
      return this.mealService.calculateTotalNutrients(meal.foods);
    }
    return { calories: 0, proteins: 0, fats: 0, carbs: 0 };
  }


  getAge(birthday: string): number {
    const birthDate = new Date(birthday);
    const today = new Date();
    let age = today.getFullYear() - birthDate.getFullYear();
    if (
      today.getMonth() < birthDate.getMonth() ||
      (today.getMonth() === birthDate.getMonth() && today.getDate() < birthDate.getDate())
    ) {
      age--;
    }
    return age;
  }

  getDashArray(currentCalories: number, totalCalories: number): string {
    const radius = 70;
    const circumference = 2 * Math.PI * radius;

    const progress = totalCalories === 0 ? 0 : (currentCalories * 100 / totalCalories);

    const dashArray = (progress / 100) * circumference;

    return `${dashArray}, ${circumference}`;
  }

  getPercentage(current: number, total: number): number {
    return total !== 0 ? (current / total) * 100 : 0;
  }

  isModalOpen = false;
  isDropdownVisible = false;
  isDishDropdownVisible = false;

  selectedIngredients: { ingredient: Ingredient, weight: number }[] = [];
  selectedDishes: { dish: Dish, servings: number }[] = [];

  ingredients: Ingredient[] = [];
  dishes: Dish[] = [];

  mealToCreate: CreateMealDTO = new CreateMealDTO();


  loadIngredients() {
    if (this.profile) {
      this.ingredientService.getIngredients(this.profile.id).subscribe(
        (ingredients) => {
          this.ingredients = ingredients;
        },
        (error) => {
          console.error('Ошибка при загрузке ингредиентов', error);
        }
      );
    }
  }

  loadDishes() {
    if (this.profile) {
      this.dishService.getAllDishes(this.profile.id).subscribe(
        (dishes) => {
          this.dishes = dishes;
        },
        (error) => {
          console.error('Ошибка при загрузке блюд', error);
        }
      );
    }

  }

  openMealDetails(meal: any): void {
    const dialogRef = this.dialog.open(MealDetailsModalComponent, {
      width: 'auto',
      maxWidth: '800px',
      data: { meal: meal }
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log('Диалог закрыт');
    });
  }

  openModal() {
    this.isModalOpen = true;
    this.loadIngredients();
    this.loadDishes();
    this.selectedDishes = []
    this.selectedIngredients = []
    this.mealToCreate = new CreateMealDTO();
  }

  closeModal() {
    this.isModalOpen = false;
    this.isDropdownVisible = false;
    this.isDishDropdownVisible = false;
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
    if (this.dayResult) {
      if (this.selectedIngredients.length === 0 && this.selectedDishes.length === 0) {
        this.closeModal();
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
      this.mealToCreate.dayResultId = this.dayResult.id;
      
      this.mealService.addMeal(this.mealToCreate).subscribe(
        (meal) => {
          console.log("meal was created", meal)
          this.dayResult?.meals.push(meal)
          this.updateTotalNutrients()
          this.closeModal()
        },
        (error) => {
          console.error("Error while creating meal:", error)
        }
      )
    }
  }

  updateTotalNutrients(): void {
    if (!this.dayResult?.meals) return;

    let totalCalories = 0;
    let totalProteins = 0;
    let totalFats = 0;
    let totalCarbs = 0;

    this.dayResult.meals.forEach((meal) => {
      if (meal.foods) {
        const mealNutrients = this.mealService.calculateTotalNutrients(meal.foods);
        totalCalories += mealNutrients.calories;
        totalProteins += mealNutrients.proteins;
        totalFats += mealNutrients.fats;
        totalCarbs += mealNutrients.carbs;
      }
    });


    this.totalNutrients = {
      calories: totalCalories,
      proteins: totalProteins,
      fats: totalFats,
      carbohydrates: totalCarbs,
    };
  }

  removeMeal(mealId: number): void {
    if (this.dayResult) {
      this.mealService.deleteMeal(mealId, this.dayResult.id).subscribe(
        () => {
          if (this.dayResult) {
            this.dayResult.meals = this.dayResult.meals.filter(meal => meal.id !== mealId);

            // Обновление значений КБЖУ
            this.updateTotalNutrients();

            // Если Angular не видит изменений, можно обновить ссылку:
            this.dayResult = { ...this.dayResult };
            console.log("Deleted successfully");
          }
        },
        (error) => {
          console.error("Error while deleting", error);
        }
      );
    }
  }


  adjustWaterGoal(action: 'increase' | 'decrease'): void {
    if (!this.profile) {
      console.error('Profile is not defined');
      return;
    }

    if (action === 'increase') {
      this.profile.desiredGlassesOfWater++;
    } else if (action === 'decrease' && this.profile.desiredGlassesOfWater > 0) {
      this.profile.desiredGlassesOfWater--;
    }
    let profile: UpdateProfileDTO =
    {
      id: this.profile.id,
      name: undefined,
      weight: undefined,
      height: undefined,
      activityLevel: undefined,
      desiredGlassesOfWater: this.profile.desiredGlassesOfWater
    }

    this.profileService.updateProfile(profile).subscribe(
      () => {
        console.log('Water goal updated successfully:', profile);
      },
      (error) => {
        console.error('Error updating water goal:', error);
      }
    );
  }

  drinkWater(): void {

    if (!this.dayResult) {
      console.error('No day result available for drinking water.');
      return;
    }

    const updateDayResultDTO: UpdateDayResultDTO = {
      id: this.dayResult.id,
      glassesOfWater: this.dayResult.glassesOfWater + 1,
      weight: this.dayResult.weight,
      height: this.dayResult.height,
      activityLevel: this.dayResult.activityLevel,
    };

    this.dayResultService.updateDayResult(updateDayResultDTO).subscribe({
      next: () => {
        console.log('Glass of water added.');
        this.dayResult!.glassesOfWater += 1;
      },
      error: (err) => {
        console.error('Error adding glass of water:', err);
      },
    });
  }
}
