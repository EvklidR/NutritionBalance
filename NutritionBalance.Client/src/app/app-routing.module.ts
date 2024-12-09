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
import { MealPlan } from './models/mealPlan/entities/meal-plan.model';

  const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'profile-selection', component: ProfileSelectionComponent },
    { path: 'create-profile', component: CreateProfileComponent },
    { path: 'profile', component: ProfileInfoComponent },
    { path: 'food', component: ProfileIngredientsComponent },
    { path: 'dishes', component: ProfileDishesComponent },
    { path: 'meal-plans', component: MealPlansComponent },
    { path: '**', redirectTo: '' },
  ];

  @NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }
