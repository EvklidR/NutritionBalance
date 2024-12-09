import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInterceptor } from '../app/services/interceptors/token.interceptor';
import { FormsModule } from '@angular/forms';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { NgxChartsModule } from '@swimlane/ngx-charts';

import { SidebarComponent } from './components/sideBar/sidebar.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';
import { HomeComponent } from './components/home/home.component';
import { ProfileSelectionComponent } from './components/profile-selection/profile-selection.component';
import { CreateProfileComponent } from './components/create-profile/create-profile.component';
import { MealDetailsModalComponent } from './components/meal-details-modal/meal-details-modal.component';
import { ProfileInfoComponent } from './components/profile-info/profile-info.component';
import { ProfileIngredientsComponent } from './components/profile-ingredients/profile-ingredients.component';
import { AddIngredientModalComponent } from './components/add-ingredient-modal/add-ingredient-modal.component';
import { ProfileDishesComponent } from './components/profile-dishes/profile-dishes.component';
import { AddDishModalComponent } from './components/add-dish-modal/add-dish-modal.component';
import { DishIngredientsModalComponent } from './components/dish-ingredients-modal/dish-ingredients-modal.component';
import { MealPlansComponent } from './components/meal-plans/meal-plans.component'

@NgModule({
  declarations: [
    AppComponent,
    SidebarComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    ProfileSelectionComponent,
    CreateProfileComponent,
    MealDetailsModalComponent,
    ProfileInfoComponent,
    ProfileIngredientsComponent,
    AddIngredientModalComponent,
    ProfileDishesComponent,
    AddDishModalComponent,
    DishIngredientsModalComponent,
    MealPlansComponent,
    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MatSnackBarModule,
    BrowserAnimationsModule,
    FontAwesomeModule,
    NgxChartsModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
