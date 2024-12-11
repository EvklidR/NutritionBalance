import { Component, OnInit } from '@angular/core';
import { DayResultService } from '../../services/profile/day-result.service';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { Profile } from '../../models/profile/entities/profile.model';
import { Subscription } from 'rxjs';
import { ProfileService } from '../../services/profile/profile.service';
import { DayResult } from '../../models/profile/entities/day-result.model';
import { sub, format, addDays } from 'date-fns';
import { jsPDF } from "jspdf";
import addFont from "jspdf";
import 'jspdf-autotable';
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-food-statistics',
  templateUrl: './food-statistics.component.html',
  styleUrls: ['./food-statistics.component.css']
})
export class FoodStatisticsComponent implements OnInit {
  profile!: Profile | null;
  private profileSubscription!: Subscription;

  dayResults: DayResult[] = [];
  weeksAgo: number = 1;
  availablePeriods: number[] = [1, 2, 3, 4, 8, 12, 18, 24];

  start: Date = sub(new Date(), { weeks: this.weeksAgo });
  end: Date = new Date();

  startDate: string = format(this.start, 'yyyy-MM-dd');
  endDate: string = format(this.end, 'yyyy-MM-dd');

  caloriesData: { date: string, calories: number }[] = [];
  macrosData: { date: string, proteins: number, fats: number, carbs: number }[] = [];

  chartData: any[] = [];
  colorScheme = 'cool';
  viewMode: 'calories' | 'macros' = 'calories'; // Toggle between calories and macros
  yScaleMin = 0;
  yScaleMax: number = 0;

  constructor(
    private router: Router,
    private authService: AuthService,
    private profileService: ProfileService,
    private dayResultService: DayResultService
  ) { }

  ngOnInit(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }

    this.profileSubscription = this.profileService.currentProfile$.subscribe((profile) => {
      this.profile = profile;

      if (profile) {
        this.loadDayResults(profile.id);
      }
    });
  }

  loadDayResults(profileId: number): void {
    this.dayResultService.getDayResultsByPeriod(profileId, this.startDate, this.endDate).subscribe(
      (days) => {
        this.dayResults = days;
        this.generateData();
      }
    );
  }

  generateData(): void {
    this.caloriesData = [];
    this.macrosData = [];

    for (let currentDate = addDays(this.start, 1); currentDate <= this.end; currentDate = addDays(currentDate, 1)) {
      const formattedDate = format(currentDate, 'yyyy-MM-dd');
      const dayResult = this.dayResults.find(d => d.date === formattedDate);

      if (dayResult) {
        const totalCalories = dayResult.meals.reduce((sum, meal) =>
          sum + meal.foods.reduce((mealSum, food) => mealSum + (food.food.calories * food.weight) / 100, 0), 0);

        const totalProteins = dayResult.meals.reduce((sum, meal) =>
          sum + meal.foods.reduce((mealSum, food) => mealSum + (food.food.proteins * food.weight) / 100, 0), 0);

        const totalFats = dayResult.meals.reduce((sum, meal) =>
          sum + meal.foods.reduce((mealSum, food) => mealSum + (food.food.fats * food.weight) / 100, 0), 0);

        const totalCarbs = dayResult.meals.reduce((sum, meal) =>
          sum + meal.foods.reduce((mealSum, food) => mealSum + (food.food.carbohydrates * food.weight) / 100, 0), 0);

        this.caloriesData.push({ date: formattedDate, calories: totalCalories });
        this.macrosData.push({ date: formattedDate, proteins: totalProteins, fats: totalFats, carbs: totalCarbs });
      } else {
        this.caloriesData.push({ date: formattedDate, calories: 0 });
        this.macrosData.push({ date: formattedDate, proteins: 0, fats: 0, carbs: 0 });
      }
    }

    this.updateChartData();
    this.generateTopFoods();
  }

  updateChartData(): void {
    if (this.viewMode === 'calories') {
      this.chartData = [
        {
          name: 'Calories',
          series: this.caloriesData.map(item => ({ name: item.date, value: item.calories }))
        }
      ];
    } else if (this.viewMode === 'macros') {
      this.chartData = [
        {
          name: 'Proteins',
          series: this.macrosData.map(item => ({ name: item.date, value: item.proteins }))
        },
        {
          name: 'Fats',
          series: this.macrosData.map(item => ({ name: item.date, value: item.fats }))
        },
        {
          name: 'Carbohydrates',
          series: this.macrosData.map(item => ({ name: item.date, value: item.carbs }))
        }
      ];
    }
  }

  navigateToBodyStatistics() {
    this.router.navigate(['/statistics']);
  }

  updatePeriod(): void {
    if (this.profile) {
      this.start = sub(new Date(), { weeks: this.weeksAgo });
      this.loadDayResults(this.profile.id);
    }
  }

  switchViewMode(mode: 'calories' | 'macros'): void {
    this.viewMode = mode;
    this.updateChartData();
  }

  topFoods: { name: string, weight: number, calories: number, proteins: number, fats: number, carbs: number }[] = [];

  generateTopFoods(): void {
    const foodMap = new Map<string, { weight: number, calories: number, proteins: number, fats: number, carbs: number }>();

    for (const dayResult of this.dayResults) {
      for (const meal of dayResult.meals) {
        for (const food of meal.foods) {
          const foodName = food.food.name;
          const weight = food.weight;
          const calories = (food.food.calories * weight) / 100;
          const proteins = (food.food.proteins * weight) / 100;
          const fats = (food.food.fats * weight) / 100;
          const carbs = (food.food.carbohydrates * weight) / 100;

          if (foodMap.has(foodName)) {
            const existing = foodMap.get(foodName)!;
            foodMap.set(foodName, {
              weight: existing.weight + weight,
              calories: existing.calories + calories,
              proteins: existing.proteins + proteins,
              fats: existing.fats + fats,
              carbs: existing.carbs + carbs,
            });
          } else {
            foodMap.set(foodName, { weight, calories, proteins, fats, carbs });
          }
        }
      }
    }

    // Convert Map to array and sort by weight
    this.topFoods = Array.from(foodMap.entries())
      .map(([name, stats]) => ({ name, ...stats }))
      .sort((a, b) => b.weight - a.weight)
      .slice(0, 15); // Take only top 15 foods
  }

  exportToPDF(): void {
    const doc = new jsPDF('l', 'mm', 'a4'); // 'l' for landscape orientation
    const userName = this.profile ? this.profile.name : 'Unknown User';

    const header = `User Report: ${userName}`;

    doc.text(header, 10, 10);

    html2canvas(document.querySelector('ngx-charts-line-chart')!).then(canvas => {
      const imgData = canvas.toDataURL('image/png');
      const imgWidth = 297;
      const imgHeight = (canvas.height * imgWidth) / canvas.width;

      doc.addImage(imgData, 'JPEG', 0, 20, imgWidth, imgHeight); // Stretch the image to full width

      doc.addPage('a4','p'); // Add a new page with portrait orientation
      doc.text('Top 15 Foods:', 10, 10);

      const headers = ['Food', 'Weight (g)', 'Calories (kcal)', 'Proteins (g)', 'Fats (g)', 'Carbohydrates (g)'];
      const rows = this.topFoods.map(food => [
        food.name,
        food.weight.toFixed(0),
        food.calories.toFixed(0),
        food.proteins.toFixed(1),
        food.fats.toFixed(1),
        food.carbs.toFixed(1)
      ]);

      (doc as any).autoTable({
        head: [headers],
        body: rows,
        startY: 20,
      });

      doc.save('Report.pdf');
    });
  }
}
