import { Component, OnInit } from '@angular/core';
import { MealPlanService } from '../../services/meal-plan.service';
import { MealPlan } from '../../models/mealPlan/entities/meal-plan.model';
import { MealPlanType } from '../../models/mealPlan/enums/meal-plan-type.enum';
import { ProfileService } from '../../services/profile/profile.service'
import { Profile } from '../../models/profile/entities/profile.model';
import { Subscription } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-meal-plans',
  templateUrl: './meal-plans.component.html',
  styleUrls: ['./meal-plans.component.css'],
})
export class MealPlansComponent implements OnInit {
  userRole: string | null = null;

  profile: Profile | null = null;
  profileId: number = 0;
  private profileSubscription!: Subscription;

  mealPlanCategories = Object.values(MealPlanType).filter(value => typeof value === 'number') as number[];
  mealPlansByCategory: { [key: number]: MealPlan[] } = {};
  currentPageByCategory: { [key: number]: number } = {};
  expandedPlanId: number | null = null;

  choosenMealPlan: MealPlan | null = null;

  isLoading: boolean = false;

  mealPlanImages: { [planId: number]: string } = {};

  mealPlanType = MealPlanType;

  constructor(
    private authService: AuthService,
    private router: Router,
    private mealPlanService: MealPlanService,
    private profileService: ProfileService
  ) { }

  ngOnInit(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }

    if (this.authService.isAdmin()) {
      this.userRole = 'admin'
    }


    this.profileSubscription = this.profileService.currentProfile$.subscribe((profile) => {
      this.profile = profile;

      console.log(this.profile, this.userRole)

      if (profile) {
        this.profileId = profile.id;
        this.mealPlanCategories.forEach(category => {
          this.currentPageByCategory[category] = 1;
          this.loadMealPlansForCategory(category);
        });
      }
      if (profile?.mealPlanId) {
        this.loadChoosenMealPlan(profile.mealPlanId)
      }
    })
  }

  loadMealPlansForCategory(category: MealPlanType): void {
    const pageNumber = this.currentPageByCategory[category];
    const pageSize = 3;

    this.isLoading = true;
    this.mealPlanService
      .getMealPlansByCategory(category, pageNumber, pageSize)
      .subscribe(
        (data) => {

          if (data.length == 0 && this.currentPageByCategory[category] != 1) {
            this.currentPageByCategory[category] = 1
            this.loadMealPlansForCategory(category)
          }

          this.mealPlansByCategory[category] = data || [];
          this.isLoading = false;

          this.mealPlansByCategory[category].forEach(plan => {
            if (plan.imageUrl && !this.mealPlanImages[plan.id]) {
              this.loadImage(plan.id, plan.imageUrl);
            }
          });
        },
        (error) => {
          console.error(`Ошибка загрузки планов питания для категории ${category}:`, error);
          this.isLoading = false;
        }
      );
  }

  loadImage(planId: number, imageUrl: string): void {
    this.mealPlanService.getFile(imageUrl).subscribe(
      (blob) => {
        const reader = new FileReader();
        reader.onload = () => {
          this.mealPlanImages[planId] = reader.result as string;
        };
        reader.readAsDataURL(blob);
      },
      (error) => {
        console.error(`Ошибка загрузки изображения для плана питания с ID ${planId}:`, error);
      }
    );
  }

  loadChoosenMealPlan(mealPlanId: number): void {
    this.mealPlanService.getMealPlansById(mealPlanId).subscribe(
      (mealPlan) => {
        this.choosenMealPlan = mealPlan;
      },
      (err) => {
        console.log("Не удалось загрузить текущий план")
      }
    )
  }

  nextPage(category: MealPlanType): void {
    this.currentPageByCategory[category]++;
    this.loadMealPlansForCategory(category);
  }

  prevPage(category: MealPlanType): void {
    if (this.currentPageByCategory[category] > 1) {
      this.currentPageByCategory[category]--;
      this.loadMealPlansForCategory(category);
    }
  }

  toggleDescription(planId: number): void {
    this.expandedPlanId = this.expandedPlanId === planId ? null : planId;
  }

  getCategoryName(category: MealPlanType): string {
    switch (category) {
      case MealPlanType.WeightLoss:
        return 'Потеря веса';
      case MealPlanType.WeightGain:
        return 'Набор веса';
      case MealPlanType.MuscleGain:
        return 'Наращивание мышц';
      case MealPlanType.Maintenance:
        return 'Поддержание формы';
      default:
        return 'Неизвестная категория';
    }
  }

  selectPlan(plan: MealPlan, event: Event): void {
    event.stopPropagation();
    this.profileService.chooseMealPlan(this.profileId, plan.id).subscribe(
      () => {
        console.log("Новая подписка успешна")
        this.choosenMealPlan = plan;
        this.profile!.mealPlanId = plan.id;
      }
    )
  }

  navigateToTest(): void {
    this.router.navigate(['/test']);
  }

  navigateToMyPlans(): void {
    this.router.navigate(['/my-meal-plans']);
  }
}
