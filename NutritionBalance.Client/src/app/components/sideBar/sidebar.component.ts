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
  profiles$!: Observable<Profile[]>; // Отложенная инициализация
  currentUser$!: Observable<Profile | null>; // Отложенная инициализация
  showProfileList: boolean = false;

  constructor(
    private router: Router,
    private authService: AuthService,
    private profileService: ProfileService,
    private elRef: ElementRef, // Доступ к элементам DOM
    private renderer: Renderer2 // Для добавления и удаления событий
  ) { }

  ngOnInit(): void {
    this.profiles$ = this.profileService.profiles$;
    this.currentUser$ = this.profileService.currentProfile$;

    this.profileService.getUserProfiles().subscribe(() => {
      this.profileService.loadCurrentProfile();
    });

    // Слушаем клики по документу
    this.renderer.listen('document', 'click', (event) => {
        this.showProfileList = false;
    });
  }


  toggleProfileList(event: MouseEvent): void {
    event.stopPropagation();
    this.showProfileList = !this.showProfileList;
  }

  selectProfile(profile: Profile, event: MouseEvent): void {
    event.stopPropagation(); // Останавливаем распространение события

    this.profileService.setCurrentProfile(profile);
    this.showProfileList = false; // Закрыть список после выбора
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
