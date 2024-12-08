import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-meal-details-modal',
  templateUrl: './meal-details-modal.component.html',
  styleUrls: ['./meal-details-modal.component.css']
})
export class MealDetailsModalComponent {

  constructor(@Inject(MAT_DIALOG_DATA) public data: any) { }

  closeDialog(): void {
    this.data.dialogRef.close();
  }
}
