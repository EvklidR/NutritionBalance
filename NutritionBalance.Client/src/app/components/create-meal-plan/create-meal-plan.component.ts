import { Component, OnInit } from '@angular/core';
import { MealPlanCreateDTO } from '../../models/mealPlan/DTOs/meal-plan-create.dto';
import { MealPlanType } from '../../models/mealPlan/enums/meal-plan-type.enum';
import { NutrientType } from '../../models/mealPlan/enums/nutrient-type.enum';
import { CalculationType } from '../../models/mealPlan/enums/calculation-type.enum';
import { MealPlanService } from '../../services/meal-plan.service'
import { Router } from '@angular/router';
import { MealPlan } from '../../models/mealPlan/entities/meal-plan.model';

@Component({
  selector: 'app-create-meal-plan',
  templateUrl: './create-meal-plan.component.html',
  styleUrls: ['./create-meal-plan.component.css']
})
export class CreateMealPlanComponent implements OnInit {
  mealPlan: MealPlanCreateDTO = new MealPlanCreateDTO();
  nutrientTypes = ['Белки', 'Жиры', 'Углеводы'];
  calculationTypes = CalculationType;
  mealPlanType = MealPlanType

  imageUrl?: string;
  selectedImage: File | null = null;
  constructor(
    private router: Router,
    private mealPlanService: MealPlanService
  ) {}

  ngOnInit(): void {
    this.mealPlan.type = MealPlanType.Maintenance;

    this.mealPlan.days.push({
      numberOfDay: 1,
      caloriePercentage: 1,
      nutrientsOfDay: [
        { nutrientType: NutrientType.Protein, calculationType: CalculationType.Percent, value: 0.2 },
        { nutrientType: NutrientType.Fat, calculationType: CalculationType.Percent, value: 0.3 },
        { nutrientType: NutrientType.Carbohydrate, calculationType: CalculationType.Percent, value: 0.5 }
      ]
    });
  }

  addDay() {
    const newDay = {
      numberOfDay: this.mealPlan.days.length + 1,
      caloriePercentage: 1,
      nutrientsOfDay: [
        { nutrientType: NutrientType.Protein, calculationType: CalculationType.Percent, value: 0.2 },
        { nutrientType: NutrientType.Fat, calculationType: CalculationType.Percent, value: 0.3 },
        { nutrientType: NutrientType.Carbohydrate, calculationType: CalculationType.Percent, value: 0.5 }
      ]
    };
    this.mealPlan.days.push(newDay);
  }

  deleteDay(dayIndex: number) {
    if (this.mealPlan.days.length > 1) {
      this.mealPlan.days.splice(dayIndex, 1);
      this.mealPlan.days.forEach((day, index) => {
        day.numberOfDay = index + 1;
      });
    } else {
      alert('Невозможно удалить последний день!');
    }
  }

  onSubmit() {
    this.mealPlan.type = Number(this.mealPlan.type);

    console.log(this.mealPlan);
    this.mealPlanService.createMealPlan(this.mealPlan).subscribe(
      (plan) => {
        if (this.selectedImage) {
          this.mealPlanService.updateFile(plan.id, this.selectedImage).subscribe(
            () => {
              console.log("картинка добавлена")
            },
            (error) => {
              console.error('Ошибка при обновлении изображения:', error);
            }
          );
        }
        console.log("создан ", plan)
        this.router.navigate(["/my-meal-plans"])
      }
    )
  }

  onImageChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedImage = file;
      this.imageUrl = URL.createObjectURL(file);
    }
  }

  getCalculationDescription(calculationType: CalculationType): string {
    switch (calculationType) {
      case CalculationType.Fixed:
        return 'Фиксированное значение';
      case CalculationType.Percent:
        return 'Процент от общей калорийности';
      case CalculationType.PerKg:
        return 'Значение на килограмм массы';
      case CalculationType.ByDefault:
        return 'Оставшееся количество после других нутриентов';
      default:
        return '';
    }
  }
}
