import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { ExpensesClient, GroupsClient, MembersClient, SettlementsClient, UsersClient } from './splitwiseAPI';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers: [
    UsersClient,
    GroupsClient,
    SettlementsClient,
    MembersClient,
    ExpensesClient
  ]
})
export class ApiModule { }
