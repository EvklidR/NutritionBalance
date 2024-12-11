import { Component, OnInit } from '@angular/core'
import { DayResultService } from '../../services/profile/day-result.service'
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { Profile } from '../../models/profile/entities/profile.model';
import { Observable, Subscription } from 'rxjs';
import { ProfileService } from '../../services/profile/profile.service';
import { DayResult } from '../../models/profile/entities/day-result.model';
import { parseISO, eachDayOfInterval, format, sub, addDays } from 'date-fns';
import { curveLinear, curveCardinal, curveMonotoneX, curveMonotoneY } from 'd3-shape';

@Component({
  selector: 'app-statistics',
  templateUrl: './statistics.component.html',
  styleUrl: './statistics.component.css'
})
export class StatisticsComponent implements OnInit {
  profile!: Profile | null;
  private profileSubscription!: Subscription;

  dayResults: DayResult[] = [];

  weeksAgo: number = 1;
  availablePeriods: number[] = [1, 2, 3, 4, 8, 12, 18, 24];


  start: Date = sub(new Date(), { weeks: this.weeksAgo });
  end: Date = new Date();

  startDate: string = format(this.start, 'yyyy-MM-dd');
  endDate: string = format(this.end, 'yyyy-MM-dd');

  weightData: { date: string, weight: number }[] = [];
  imtData: { date: string, imt: number }[] = [];

  chartData: any[] = [];
  colorScheme: string = 'cool';
  curveType = curveMonotoneX;

  yScaleMin = 0;
  yScaleMax: number = 0;

  averageWeight: number = 0;
  weightDifference: number = 0;

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
        console.log(this.dayResults);
        this.weightData = [];
        this.imtData = [];
        this.generateData();
      }
    );
  }

  generateData(): void {
    for (let currentDate = addDays(this.start, 1); currentDate <= this.end; currentDate = addDays(currentDate, 1)) {
      const formattedDate = format(currentDate, 'yyyy-MM-dd');
      let day: DayResult | undefined = this.dayResults.find(d => d.date === formattedDate);
      this.imtData.push({ date: formattedDate, imt: -1 });
      this.weightData.push({ date: formattedDate, weight: day?.weight ?? -1 });
    }

    const hasKnownWeight = this.weightData.some(item => item.weight > -1);

    if (!hasKnownWeight) {
      const currentWeight = this.profile!.weight;
      this.weightData = this.weightData.map(item => ({ ...item, weight: currentWeight }));
    } else {
      let currentValue: number = -1;
      let earliestValue: number = -1;
      for (let i = 0; i < this.weightData.length; i++) {
        if (this.weightData[i].weight > -1) {
          currentValue = this.weightData[i].weight;
          if (earliestValue == -1) {
            earliestValue = this.weightData[i].weight;
          }
        } else {
          this.weightData[i].weight = currentValue;
        }
      }

      for (let i = 0; i < this.weightData.length; i++) {
        if (this.weightData[i].weight == -1) {
          this.weightData[i].weight = earliestValue;
        } else {
          break;
        }
      }
    }

    for (let i = 0; i < this.imtData.length; i++) {
      this.imtData[i].imt = this.weightData[i].weight / (this.profile!.height * this.profile!.height / 10000);
    }

    // Рассчитываем среднее значение и разницу
    const knownWeights = this.weightData.filter(item => item.weight > -1);
    if (knownWeights.length > 0) {
      this.averageWeight = knownWeights.reduce((sum, item) => sum + item.weight, 0) / knownWeights.length;
      const firstWeight = knownWeights[0].weight;
      const lastWeight = knownWeights[knownWeights.length - 1].weight;
      this.weightDifference = lastWeight - firstWeight;
    }

    // Динамически рассчитываем максимальное значение для оси Y
    const maxWeight = Math.max(...this.weightData.map(item => item.weight));
    const maxImt = Math.max(...this.imtData.map(item => item.imt));
    this.yScaleMax = Math.max(maxWeight, maxImt) + 10; // Мы берем наибольшее значение для максимума

    // Создаем данные для графика
    this.chartData = [
      {
        name: 'Вес (кг)',
        series: this.weightData.map(item => ({
          name: item.date,
          value: item.weight
        }))
      },
      {
        name: 'ИМТ',
        series: this.imtData.map(item => ({
          name: item.date,
          value: item.imt
        }))
      }
    ];

    console.log(this.weightData); // Проверка данных
    console.log(this.chartData);  // Проверка данных для графика
  }

  navigateToFoodStatistics(): void {
    console.log("here")
    this.router.navigate(["/food-statistics"])
  }

  updatePeriod(): void {
    if (this.profile) {
      this.start = sub(new Date(), { weeks: this.weeksAgo });
      this.loadDayResults(this.profile.id);
    }
  }
}
