import { ActivityLevel } from '../../enums/activity-level.enum';

export class UpdateProfileDTO {
  id!: number;
  name!: string;
  weight!: number;
  height!: number;
  activityLevel!: ActivityLevel;
  desiredGlassesOfWater!: number;
}
