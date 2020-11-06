import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { UsersService } from 'src/app/core/api/setup/api';
import { User } from 'src/app/model/user';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {


  User: User;
  errorMessage: any;

  constructor(
    private router: Router,
    private userService: UsersService
  ) { }

  ngOnInit(): void {
  }


  AddUser(form: NgForm) {
    this.User = form.value;
    console.log(this.User);
    this.userService.register(this.User).subscribe({
      error: err => this.errorMessage = err,
      complete: () => this.RouteToLogin()
    });
  }

  RouteToLogin() {
    this.router.navigate(['/login']);
  }
}
