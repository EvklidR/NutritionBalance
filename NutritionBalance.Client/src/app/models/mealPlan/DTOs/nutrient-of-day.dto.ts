import { NutrientType } from '../enums/nutrient-type.enum';
import { CalculationType } from '../enums/calculation-type.enum';

export class NutrientOfDayDTO {
  nutrientType!: NutrientType;
  calculationType!: CalculationType;
  value?: number;
}
