import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { SidebarComponent } from './components/sideBar/sidebar.component';
import { HomeComponent } from './components/home/home.component'
import { ProfileSelectionComponent } from './components/profile-selection/profile-selection.component';
import { CreateProfileComponent } from './components/create-profile/create-profile.component';
import { AuthGuard } from './guards/auth.guard'
import { ProfileInfoComponent } from './components/profile-info/profile-info.component'
import { ProfileIngredientsComponent } from './components/profile-ingredients/profile-ingredients.component'
import { ProfileDishesComponent } from './components/profile-dishes/profile-dishes.component'
import { MealPlansComponent } from './components/meal-plans/meal-plans.component'
import { StatisticsComponent } from './components/statistics/statistics.component';
import { FoodStatisticsComponent } from './components/food-statistics/food-statistics.component'
import { MyMealPlansComponent } from './components/my-meal-plans/my-meal-plans.component'
import { CreateMealPlanComponent } from './components/create-meal-plan/create-meal-plan.component'
import { TestPageComponent } from './components/test-page/test-page.component'

  const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'profile-selection', component: ProfileSelectionComponent },
    { path: 'create-profile', component: CreateProfileComponent },
    { path: 'profile', component: ProfileInfoComponent, canActivate: [AuthGuard] },
    { path: 'food', component: ProfileIngredientsComponent, canActivate: [AuthGuard] },
    { path: 'dishes', component: ProfileDishesComponent, canActivate: [AuthGuard] },
    { path: 'meal-plans', component: MealPlansComponent, canActivate: [AuthGuard] },
    { path: 'my-meal-plans', component: MyMealPlansComponent },
    { path: 'statistics', component: StatisticsComponent, canActivate: [AuthGuard] },
    { path: 'food-statistics', component: FoodStatisticsComponent, canActivate: [AuthGuard] },
    { path: 'create-meal-plan', component: CreateMealPlanComponent },
    { path: 'test', component: TestPageComponent },
    { path: '**', redirectTo: '' },
  ];

  @NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }
