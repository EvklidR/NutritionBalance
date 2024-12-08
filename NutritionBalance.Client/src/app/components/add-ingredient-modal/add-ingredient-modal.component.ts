import { Component, EventEmitter, Output } from '@angular/core';
import { IngredientService } from '../../services/profile/ingredient.service';
import { CreateIngredientDTO } from '../../models/profile/DTOs/ingredient/create-ingredient.dto';


@Component({
  selector: 'app-add-ingredient-modal',
  templateUrl: './add-ingredient-modal.component.html',
  styleUrls: ['./add-ingredient-modal.component.css'],
})
export class AddIngredientModalComponent {
  @Output() ingredientAdded = new EventEmitter<void>();
  @Output() closeModal = new EventEmitter<void>();

  name: string = '';
  proteins: number | null = null;
  fats: number | null = null;
  carbohydrates: number | null = null;

  isSaving: boolean = false;

  constructor(private ingredientService: IngredientService) { }

  saveIngredient(): void {
    if (!this.name || this.proteins === null || this.fats === null || this.carbohydrates === null) {
      alert('Пожалуйста, заполните все поля.');
      return;
    }

    const newIngredient: CreateIngredientDTO = {
      profileId: 1,
      name: this.name,
      proteins: this.proteins,
      fats: this.fats,
      carbohydrates: this.carbohydrates,
    };

    this.isSaving = true;

    this.ingredientService.createIngredient(newIngredient).subscribe(
      () => {
        this.isSaving = false;
        this.ingredientAdded.emit();
        this.closeModal.emit();
      },
      (error) => {
        console.error('Ошибка при сохранении ингредиента:', error);
        this.isSaving = false;
      }
    );
  }

  closeOnBackdropClick(event: MouseEvent): void {
    this.closeModal.emit();
  }

  preventClose(event: MouseEvent): void {
    event.stopPropagation();
  }

  close(): void {
    this.closeModal.emit();
  }
}
