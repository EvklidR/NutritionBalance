  import { NgModule } from '@angular/core';
  import { RouterModule, Routes } from '@angular/router';
  import { LoginComponent } from './components/login/login.component';
  import { RegisterComponent } from './components/register/register.component';
  import { SidebarComponent } from './components/sideBar/sidebar.component';
  import { HomeComponent } from './components/home/home.component'
  import { ProfileSelectionComponent } from './components/profile-selection/profile-selection.component';
  import { CreateProfileComponent } from './components/create-profile/create-profile.component';
  import { AuthGuard } from './guards/auth.guard'

  const routes: Routes = [
    { path: 'login', component: LoginComponent },
    { path: 'register', component: RegisterComponent },
    { path: '', component: HomeComponent, canActivate: [AuthGuard] },
    { path: 'profile-selection', component: ProfileSelectionComponent },
    { path: 'create-profile', component: CreateProfileComponent },
    { path: '**', redirectTo: '' },
  ];

  @NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
  })
  export class AppRoutingModule { }
