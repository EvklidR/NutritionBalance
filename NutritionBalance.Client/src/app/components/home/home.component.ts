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
import { UpdateDayResultDTO } from '../../models/profile/DTOs/dayResult/update-day-result.dto'
import { UpdateProfileDTO } from '../../models/profile/DTOs/profile/update-profile.dto'
import { MatSnackBar } from '@angular/material/snack-bar';
import { faTrash } from '@fortawesome/free-solid-svg-icons';
import { MatDialog } from '@angular/material/dialog';
import { Meal } from '../../models/profile/entities/meal.model'
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
  profileId: number = 0;
  dayResultId: number = 0;

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
        this.profileId = profile.id;
        this.currentUserId = profile.id;
        this.profileService.calculateDailyNeeds(profile.id).subscribe((dailyNeeads) => {
          this.dailyNeeads = dailyNeeads;
          this.getOrCreateDayResult();
        });
      }
    });
  }

  onMealAdded(meal: Meal): void {
    this.getOrCreateDayResult()
  }

  getOrCreateDayResult(): void {
    const today = new Date().toLocaleDateString('en-CA');

    this.dayResultSubscription = this.dayResultService.getOrCreateDayResult(this.currentUserId, today).subscribe({
      next: (result) => {
        this.dayResult = result;
        this.dayResultId = result.id;
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

  showAddMealModal: boolean = false;

 

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

  removeMeal(mealId: number, event: Event): void {
    event.stopPropagation();
    if (this.dayResult) {
      this.mealService.deleteMeal(mealId, this.dayResult.id).subscribe(
        () => {
          if (this.dayResult) {
            this.dayResult.meals = this.dayResult.meals.filter(meal => meal.id !== mealId);

            this.updateTotalNutrients();
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
