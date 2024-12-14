import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    const currentUser = localStorage.getItem('currentProfileId');
    const currentToken = localStorage.getItem('accessToken');

    if (!currentToken) {
      this.router.navigate(['/login']);
      return false;
    }

    if (!currentUser) {
      this.router.navigate(['/profile-selection']);
      return false;
    }

    return true;
  }
}
