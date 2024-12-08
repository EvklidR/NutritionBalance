import { Component, EventEmitter, Output } from '@angular/core';
import { DishService } from '../../services/profile/dish.service';
import { CreateDishDTO } from '../../models/profile/DTOs/dish/create-dish.dto';
import { IngredientOfDishDTO } from '../../models/profile/DTOs/dish/ingredient-of-dish.dto';
import { Ingredient } from '../../models/profile/entities/ingredient.model'; // Модель ингредиента
import { IngredientService } from '../../services/profile/ingredient.service'; // Сервис ингредиентов

@Component({
  selector: 'app-add-dish-modal',
  templateUrl: './add-dish-modal.component.html',
  styleUrls: ['./add-dish-modal.component.css']
})
export class AddDishModalComponent {
  @Output() dishesAdded = new EventEmitter<void>();
  @Output() closeModal = new EventEmitter<void>();

  name: string = '';
  proteins: number | null = null;
  fats: number | null = null;
  carbohydrates: number | null = null;
  description?: string;
  amountOfPortions!: number;

  allIngredients: Ingredient[] = [];
  selectedIngredients: IngredientOfDishDTO[] = [];
  selectedIngredientId: number | null = null;

  isSaving: boolean = false;

  constructor(
    private dishService: DishService,
    private ingredientService: IngredientService
  ) { }

  ngOnInit(): void {
    // Загрузка всех доступных ингредиентов
    this.ingredientService.getIngredients(1).subscribe(
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

    // Проверка на дублирование
    const exists = this.selectedIngredients.some(ing => ing.ingredientId === this.selectedIngredientId);
    if (exists) {
      alert('Этот ингредиент уже добавлен.');
      return;
    }

    // Добавление нового ингредиента в таблицу
    this.selectedIngredients.push({
      ingredientId: this.selectedIngredientId,
      weight: 0 // Значение по умолчанию
    });

    // Сброс выбора
    this.selectedIngredientId = null;
  }

  removeIngredient(ingredientId: number): void {
    this.selectedIngredients = this.selectedIngredients.filter(ing => ing.ingredientId !== ingredientId);
  }

  getIngredientName(ingredientId: number): string {
    const ingredient = this.allIngredients.find(i => i.id === ingredientId);
    return ingredient ? ingredient.name : 'Неизвестный ингредиент';
  }

  saveDish(): void {
    if (!this.name || !this.amountOfPortions) {
      alert('Пожалуйста, заполните все обязательные поля.');
      return;
    }

    const newDish: CreateDishDTO = {
      profileId: 1,
      name: this.name,
      description: this.description || '',
      amountOfPortions: this.amountOfPortions,
      ingredients: this.selectedIngredients
    };

    this.isSaving = true;

    this.dishService.createDish(newDish).subscribe(
      () => {
        this.isSaving = false;
        this.dishesAdded.emit();
        this.closeModal.emit();
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

