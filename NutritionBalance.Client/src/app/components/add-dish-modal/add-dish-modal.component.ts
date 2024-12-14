import { Component, EventEmitter, Output, Input } from '@angular/core';
import { DishService } from '../../services/profile/dish.service';
import { CreateDishDTO } from '../../models/profile/DTOs/dish/create-dish.dto';
import { IngredientOfDishDTO } from '../../models/profile/DTOs/dish/ingredient-of-dish.dto';
import { Ingredient } from '../../models/profile/entities/ingredient.model';
import { IngredientService } from '../../services/profile/ingredient.service';
import { Dish } from '../../models/profile/entities/dish.model'

@Component({
  selector: 'app-add-dish-modal',
  templateUrl: './add-dish-modal.component.html',
  styleUrls: ['./add-dish-modal.component.css']
})
export class AddDishModalComponent {
  @Input() profileId: number = 0;
  @Output() dishAdded = new EventEmitter<Dish>();
  @Output() closeModal = new EventEmitter<void>();

  name: string = '';
  proteins: number | null = null;
  fats: number | null = null;
  carbohydrates: number | null = null;
  description?: string;
  amountOfPortions!: number;
  imageUrl?: string;
  selectedImage: File | null = null;


  allIngredients: Ingredient[] = [];
  selectedIngredients: IngredientOfDishDTO[] = [];
  selectedIngredientId: number | null = null;

  isSaving: boolean = false;

  constructor(
    private dishService: DishService,
    private ingredientService: IngredientService
  ) { }

  ngOnInit(): void {
    this.ingredientService.getIngredients(this.profileId).subscribe(
      (data: Ingredient[]) => {
        this.allIngredients = data;
      },
      (error) => {
        console.error('Ошибка загрузки ингредиентов:', error);
      }
    );
  }

  addIngredient(): void {
    if (!this.selectedIngredientId) return;

    const exists = this.selectedIngredients.some(ing => ing.ingredientId === this.selectedIngredientId);
    if (exists) {
      alert('Этот ингредиент уже добавлен.');
      return;
    }
    
    this.selectedIngredients.push({
      ingredientId: this.selectedIngredientId,
      weight: 50
    });

    this.selectedIngredientId = null;
  }

  removeIngredient(ingredientId: number): void {
    this.selectedIngredients = this.selectedIngredients.filter(ing => ing.ingredientId !== ingredientId);
  }

  getIngredientName(ingredientId: number): string {
    const ingredient = this.allIngredients.find(i => i.id === ingredientId);
    return ingredient ? ingredient.name : 'Неизвестный ингредиент';
  }

  onImageChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.selectedImage = file;
      this.imageUrl = URL.createObjectURL(file);
    }
  }

  saveDish(): void {
    if (!this.name || !this.amountOfPortions) {
      alert('Пожалуйста, заполните все обязательные поля.');
      return;
    }

    const newDish: CreateDishDTO = {
      profileId: this.profileId,
      name: this.name,
      description: this.description || '',
      amountOfPortions: this.amountOfPortions,
      ingredients: this.selectedIngredients
    };

    this.isSaving = true;

    this.dishService.createDish(newDish).subscribe(
      (dish) => {
        if (this.selectedImage) {
          this.dishService.updateImage(dish.id, this.selectedImage).subscribe(
            () => {
              this.isSaving = false;
              this.dishAdded.emit(dish);
              this.closeModal.emit();
            },
            (error) => {
              console.error('Ошибка при обновлении изображения:', error);
              this.isSaving = false;
            }
          );
        } else {
          this.isSaving = false;
          this.dishAdded.emit(dish);
          this.closeModal.emit();
        }
      },
      (error) => {
        console.error('Ошибка при сохранении блюда:', error);
        this.isSaving = false;
      }
    );
  }

  preventClose(event: MouseEvent): void {
    event.stopPropagation();
  }

  close(): void {
    this.closeModal.emit();
  }
}

