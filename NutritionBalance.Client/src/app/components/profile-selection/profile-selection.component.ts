import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ProfileService } from '../../services/profile/profile.service';
import { Profile } from '../../models/profile/entities/profile.model';

@Component({
  selector: 'app-profile-selection',
  templateUrl: './profile-selection.component.html',
  styleUrls: ['./profile-selection.component.css']
})
export class ProfileSelectionComponent implements OnInit {
  profiles: Profile[] = [];

  constructor(private router: Router, private profileService: ProfileService) { }

  ngOnInit(): void {
    this.loadProfiles();
  }

  loadProfiles(): void {
    this.profileService.getUserProfiles().subscribe({
      next: (data) => {
        this.profiles = data;
      },
      error: (err) => {
        console.error('Ошибка при загрузке профилей:', err);
      }
    });
  }

  selectProfile(profile: Profile): void {
    this.profileService.setCurrentProfile(profile);
    this.router.navigate(['/home']);
  }

  createNewProfile(): void {
    this.router.navigate(['/create-profile']);
  }
}
