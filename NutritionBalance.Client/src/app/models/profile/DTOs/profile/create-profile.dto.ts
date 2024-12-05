import { ActivityLevel } from '../../enums/activity-level.enum';
import { Gender } from '../../enums/gender.enum';

export class CreateProfileDTO {
  userId?: number;
  name!: string;
  weight!: number;
  height!: number;
  birthday!: Date;
  gender!: Gender;
  activityLevel!: ActivityLevel;
}
