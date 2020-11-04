import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/model/login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: Login;

  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  Login(form: NgForm) {
    this.login = form.value;
    console.log(this.login);
    this.RouteToHome();
  }

  RouteToHome() {
    this.router.navigate(['\home']);
  }
}
