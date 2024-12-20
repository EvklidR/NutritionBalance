import { Component, OnInit, ElementRef, Renderer2 } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { ProfileService } from '../../services/profile/profile.service';
import { Profile } from '../../models/profile/entities/profile.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  profiles$!: Observable<Profile[]>;
  currentUser$!: Observable<Profile | null>;
  showProfileList: boolean = false;

  constructor(
    private router: Router,
    private authService: AuthService,
    private profileService: ProfileService,
    private elRef: ElementRef,
    private renderer: Renderer2
  ) { }

  ngOnInit(): void {
    this.profiles$ = this.profileService.profiles$;
    this.currentUser$ = this.profileService.currentProfile$;

      this.profileService.getUserProfiles().subscribe(() => {
        this.profileService.loadCurrentProfile();
      });


    this.renderer.listen('document', 'click', (event) => {
        this.showProfileList = false;
    });
  }


  toggleProfileList(event: MouseEvent): void {
    event.stopPropagation();
    this.showProfileList = !this.showProfileList;
  }

  selectProfile(profile: Profile, event: MouseEvent): void {
    event.stopPropagation();

    this.profileService.setCurrentProfile(profile);
    this.showProfileList = false;
  }


  createProfile(): void {
    this.router.navigate(['/create-profile']);
  }

  logout(): void {
    this.authService.logout();
    this.profileService.clearCurrentProfile();
    this.router.navigate(['/login']);
  }
}
