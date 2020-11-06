import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../app/core/auth/auth.guard';
import { HomeComponent } from './home/home.component';
import { PageNotFoundComponent } from './PageNotFound.component';
import { UserComponent } from './user/user.component';


const routes: Routes = [
  {
    path: '', component: UserComponent,
    data: { preload: false },
    loadChildren: () =>
      import('./user/user.module').then(m => m.UserModule)
  },
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
