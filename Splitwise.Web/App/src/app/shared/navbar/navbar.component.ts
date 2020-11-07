import { tokenName } from '@angular/compiler';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {


  AddOpenClass: string;
  AddSideNavClass: string;
  AddToCross: string;
  isLoading: string;

  constructor() { }

  ngOnInit(): void {
  }

  LogOut() {
    localStorage.removeItem('token');
    localStorage.removeItem('userName');
    localStorage.removeItem('userId');
  }

}
