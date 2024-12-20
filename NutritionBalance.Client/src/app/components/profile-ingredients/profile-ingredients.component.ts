import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { IngredientService } from '../../services/profile/ingredient.service';
import { Ingredient } from '../../models/profile/entities/ingredient.model';
import { ProfileService } from '../../services/profile/profile.service';
import { Profile } from '../../models/profile/entities/profile.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-profile-ingredients',
  templateUrl: './profile-ingredients.component.html',
  styleUrls: ['./profile-ingredients.component.css']
})
export class ProfileIngredientsComponent implements OnInit {
  profile!: Profile | null;
  profileId: number = 0;
  private profileSubscription!: Subscription;

  filterText: string = '';
  filteredIngredients: Ingredient[] = [];

  showAddIngredientModal: boolean = false;

  ingredients: Ingredient[] = [];
  isLoading: boolean = true;

  constructor(
    private authService: AuthService,
    private router: Router,
    private ingredientService: IngredientService,
    private profileService: ProfileService
  ) { }

  ngOnInit(): void {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/login']);
      return;
    }

    this.profileSubscription = this.profileService.currentProfile$.subscribe((profile) => {
      this.profile = profile;
      
      if (profile) {
        this.profileId = profile.id
        this.loadIngredients();
      }
    });
  }

  loadIngredients(): void {
    this.ingredientService.getIngredients(this.profile!.id).subscribe(
      (data: Ingredient[]) => {
        this.ingredients = data;
        this.filteredIngredients = data;
        this.isLoading = false;
      },
      (error) => {
        console.error('Ошибка загрузки ингредиентов:', error);
        this.isLoading = false;
      }
    );
  }

  navigateToMeals(): void {
    this.router.navigate(['/dishes']);
  }

  deleteIngredient(id: number): void {
    this.isLoading = true;
    this.ingredientService.deleteIngredient(id).subscribe(
      () => {
        this.ingredients = this.ingredients.filter(ingredient => ingredient.id !== id);
        this.isLoading = false;
      },
      (error) => {
        console.error('Ошибка удаления ингредиента:', error);
        this.isLoading = false;
      }
    );
  }

  onIngredientAdded(newIngredient: Ingredient): void {
    this.ingredients.push(newIngredient);
    this.filterIngredients();
  }

  filterIngredients(): void {
    const searchText = this.filterText.toLowerCase();
    this.filteredIngredients = this.ingredients.filter((ingredient) =>
      ingredient.name.toLowerCase().includes(searchText)
    );
  }

}
