import { Profile } from './profile.model'

export abstract class Food {
  id!: number;
  profileId!: number;
  name!: string;
  calories!: number;
  proteins!: number;
  fats!: number;
  carbohydrates!: number;
  profile!: Profile;
}
