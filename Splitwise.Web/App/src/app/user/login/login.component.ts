import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UsersService } from 'src/app/core/api/setup/api';
import { Login } from 'src/app/model/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: Login;
  errormessage: any;

  constructor(
    private router: Router,
    private userService: UsersService
  ) { }

  ngOnInit(): void {
    if (localStorage.getItem('token')) {
      this.RouteToHome();
    }
  }

  Login(form: NgForm) {
    this.login = form.value;
    this.userService.login(this.login).subscribe({
      next: token => this.SetToken(token),
      error: err => this.errormessage = err,
      complete: () => this.RouteToHome()
    });
  }

  SetToken(objectToken: any) {
    console.log(objectToken);
    localStorage.setItem('token', JSON.stringify(objectToken));
    const token = objectToken.token;
    const jwtData = token.split('.')[1];
    const decodedJwtJsonData = window.atob(jwtData);
    const decodedJwtData = JSON.parse(decodedJwtJsonData);
    const userId = decodedJwtData.UserID;
    localStorage.setItem('userId', userId);
    localStorage.setItem('userName', decodedJwtData.name);
  }

  RouteToHome() {
    this.router.navigate(['\home']);
  }
}
