import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { User } from 'src/app/model/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {


  User: User;

  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {
  }


  AddUser(form: NgForm) {
    this.User = form.value;
    console.log(this.User);
    this.RouteToLogin();
  }

  RouteToLogin() {
    this.router.navigate(['/login']);
  }
}
