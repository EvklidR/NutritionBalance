import { Component, OnInit } from '@angular/core';
import { MealPlanService } from '../../services/meal-plan.service';
import { MealPlan } from '../../models/mealPlan/entities/meal-plan.model';
import { MealPlanType } from '../../models/mealPlan/enums/meal-plan-type.enum';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-my-meal-plans',
  templateUrl: './my-meal-plans.component.html',
  styleUrls: ['./my-meal-plans.component.css']
})
export class MyMealPlansComponent implements OnInit {
  mealPlanCategories = Object.values(MealPlanType).filter(value => typeof value === 'number') as number[];
  mealPlansByCategory: { [key: number]: MealPlan[] } = {};
  currentPageByCategory: { [key: number]: number } = {};
  PlansToShowByCategory: { [key: number]: MealPlan[] } = {};

  expandedPlanId: number | null = null;

  isLoading: boolean = false;

  mealPlanImages: { [planId: number]: string } = {};

  mealPlanType = MealPlanType;
  pageSize: number = 3;

  constructor(
    private router: Router,
    private mealPlanService: MealPlanService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
    if (!this.authService.isAuthenticated() || !this.authService.isAdmin()) {
      this.router.navigate(['/login']);
      return;
    } else {
      this.mealPlanService.getMealPlansByOwner().subscribe(
        (plans) => {
          for (let plan of plans) {
            if (!this.mealPlansByCategory[plan.type]) {
              this.mealPlansByCategory[plan.type] = [];
              this.currentPageByCategory[plan.type] = 1;
            }

            this.mealPlansByCategory[plan.type].push(plan);

            if (plan.imageUrl && !this.mealPlanImages[plan.id]) {
              this.loadImage(plan.id, plan.imageUrl);
            }
          }

          for (const category in this.mealPlansByCategory) {
            this.updatePlansToShow(Number(category));
          }
        }
      )
    }
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

  updatePlansToShow(category: MealPlanType): void {
    const plans = this.mealPlansByCategory[category] || [];
    const page = this.currentPageByCategory[category];
    const startIndex = (page - 1) * this.pageSize;
    const endIndex = startIndex + this.pageSize;

    this.PlansToShowByCategory[category] = plans.slice(startIndex, endIndex);
  }

  nextPage(category: MealPlanType): void {
    const plans = this.mealPlansByCategory[category] || [];
    const totalPages = Math.ceil(plans.length / this.pageSize);

    if (this.currentPageByCategory[category] < totalPages) {
      this.currentPageByCategory[category]++;
      this.updatePlansToShow(category);
    }
  }

  prevPage(category: MealPlanType): void {
    if (this.currentPageByCategory[category] > 1) {
      this.currentPageByCategory[category]--;
      this.updatePlansToShow(category);
    }
  }

  canGoNext(category: MealPlanType): boolean {
    const plans = this.mealPlansByCategory[category] || [];
    const totalPages = Math.ceil(plans.length / this.pageSize);
    return this.currentPageByCategory[category] < totalPages;
  }

  canGoPrev(category: MealPlanType): boolean {
    return this.currentPageByCategory[category] > 1;
  }

  navigateToPlans() {
    this.router.navigate(["/meal-plans"])
  }

  deletePlan(plan: MealPlan, event: Event): void {
    event.stopPropagation();
    this.mealPlanService.deleteMealPlan(plan.id).subscribe(
      () => {
        console.log("Удаление успешно")
        this.mealPlansByCategory[plan.type] = this.mealPlansByCategory[plan.type].filter(p => p.id != plan.id)
        console.log(this.mealPlansByCategory[plan.type])
        this.updatePlansToShow(plan.type)
      }
    )
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

  navigateToAddingPlan() {
    this.router.navigate(["/create-meal-plan"])
  }

}
