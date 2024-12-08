import { ActivityLevel } from '../../enums/activity-level.enum';

export class UpdateDayResultDTO {
  id!: number;
  weight?: number;
  height?: number;
  activityLevel?: ActivityLevel;
  glassesOfWater!: number;
}
