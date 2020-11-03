import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavbarComponent } from './navbar/navbar.component';
import { DashboardComponent } from './dashboard/dashboard.component';



@NgModule({
  declarations: [NavbarComponent, DashboardComponent],
  imports: [
    CommonModule
  ],

  exports: [NavbarComponent, DashboardComponent]
})
export class SharedModule { }
