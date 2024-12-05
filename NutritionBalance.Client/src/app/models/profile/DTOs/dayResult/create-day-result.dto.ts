import { ActivityLevel } from '../../enums/activity-level.enum';

export class CreateDayResultDTO {
  profileId!: number;
  date!: Date;
  weight?: number;
  height?: number;
  activityLevel?: ActivityLevel;
  glassesOfWater: number = 0;
}
