import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';
import { DishService } from '../../services/profile/dish.service';
import { Dish } from '../../models/profile/entities/dish.model';
import { ProfileService } from '../../services/profile/profile.service';
import { Profile } from '../../models/profile/entities/profile.model';
import { Subscription } from 'rxjs';
import { IngredientOfDish } from '../../models/profile/entities/ingredient-of-dish.model'

@Component({
  selector: 'app-profile-dishes',
  templateUrl: './profile-dishes.component.html',
  styleUrls: ['./profile-dishes.component.css']
})
export class ProfileDishesComponent implements OnInit {
  profile!: Profile | null;
  profileId: number = 0;
  private profileSubscription!: Subscription;

  filterText: string = ''; // Поле для ввода текста фильтрации
  filteredDishes: Dish[] = []; // Отфильтрованный список блюд

  showAddDishModal: boolean = false;

  dishes: Dish[] = [];
  isLoading: boolean = true;

  constructor(
    private authService: AuthService,
    private router: Router,
    private dishService: DishService,
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
        this.profileId = profile.id;
        this.loadDishes();
      }
    });
  }

  loadDishes(): void {
    this.dishService.getAllDishes(this.profile!.id).subscribe(
      (data: Dish[]) => {
        this.dishes = data;
        this.filteredDishes = data;
        this.isLoading = false;
      },
      (error) => {
        console.error('Ошибка загрузки блюд:', error);
        this.isLoading = false;
      }
    );
  }

  navigateToIngredients(): void {
    this.router.navigate(['/food']);
  }

  deleteDish(id: number): void {
    this.isLoading = true;
    this.dishService.deleteDish(id).subscribe(
      () => {
        this.dishes = this.dishes.filter(dish => dish.id !== id);
        this.filterDishes();
        this.isLoading = false;
      },
      (error) => {
        console.error('Ошибка удаления блюда:', error);
        this.isLoading = false;
      }
    );
  }

  filterDishes(): void {
    const searchText = this.filterText.toLowerCase();
    this.filteredDishes = this.dishes.filter((dish) =>
      dish.name.toLowerCase().includes(searchText)
    );
  }

  selectedDishIngredients: IngredientOfDish[] = [];
  selectedDish: Dish = new Dish;;
  showIngredientsModal: boolean = false;

  openIngredientsModal(dish: Dish) {
    this.dishService.getDishById(dish.id).subscribe(
      (d) => {
        this.selectedDishIngredients = d.ingredients;
        this.selectedDish = d;
        this.showIngredientsModal = true;
      }
    )

  }

  onDishAdded(newDish: Dish): void {
    this.dishes.push(newDish);
    this.filterDishes();
  }

  closeModal() {
    this.showIngredientsModal = false;
  }

}
