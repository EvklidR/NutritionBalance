import { Component, Input, Output, EventEmitter } from '@angular/core';
import { IngredientOfDish } from '../../models/profile/entities/ingredient-of-dish.model';
import { Dish } from '../../models/profile/entities/dish.model'

@Component({
  selector: 'app-dish-ingredients-modal',
  templateUrl: './dish-ingredients-modal.component.html',
  styleUrls: ['./dish-ingredients-modal.component.css']
})
export class DishIngredientsModalComponent {
  @Input() ingredients: IngredientOfDish[] = [];
  @Input() dish: Dish = new Dish();
  @Output() closeModal = new EventEmitter<void>();

  onClose() {
    this.closeModal.emit();
  }
}
