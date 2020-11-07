import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Login, UsersClient } from 'src/app/core/api/splitwiseAPI';

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
    private splitwiseService: UsersClient
  ) { }

  ngOnInit(): void {
    if (localStorage.getItem('token')) {
      this.RouteToHome();
    }
  }

  Login(form: NgForm) {
    this.login = form.value;
    this.splitwiseService.login(this.login).subscribe({
      next: (token) => this.SetToken(token.data),
      error: err => this.errormessage = err,
      complete: () => this.RouteToHome()
    });
  }

  SetToken(objectToken: any) {
    console.log(objectToken);
    localStorage.setItem('token', objectToken.token);
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
