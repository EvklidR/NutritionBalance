import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { Profile } from '../../models/profile/entities/profile.model';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  profiles: Profile[] = [];
  currentUser: Profile | null = null;
  showProfileList: boolean = false;
  constructor(private router: Router, private authService: AuthService) { }

  ngOnInit(): void {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser') || 'null');

    if (!this.currentUser) {
      this.router.navigate(['/profile-selection']);
    } else {
      this.profiles = this.getProfiles();
    }
  }

  toggleProfileList(): void {
    this.showProfileList = !this.showProfileList;
  }

  selectProfile(profile: Profile): void {
    this.currentUser = profile;
    localStorage.setItem('currentUser', JSON.stringify(profile));
    this.showProfileList = false;
  }

  createProfile(): void {
    this.router.navigate(['/create-profile']);
  }

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  getProfiles(): Profile[] {
    return [
      new Profile(),
      new Profile(),
    ];
  }
}
