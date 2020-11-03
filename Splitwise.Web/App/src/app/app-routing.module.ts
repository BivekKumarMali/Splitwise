import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './user/login/login.component';
import { AuthGuard } from '../app/core/auth/auth.guard';
import { RegisterComponent } from './user/register/register.component';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './PageNotFound.component';


const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'home', component: HomeComponent, canActivate: [AuthGuard],
    data: { preload: false },
    loadChildren: () =>
      import('./home/home.module').then(m => m.HomeModule)
  },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
