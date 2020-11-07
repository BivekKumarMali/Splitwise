import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupComponent } from './group/group.component';
import { ExpenseComponent } from './expense/expense.component';
import { TransactionComponent } from './transaction/transaction.component';
import { HomeComponent } from './home.component';
import { FriendComponent } from './friend/friend.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';
import { AddgroupComponent } from './group/addgroup/addgroup.component';
import { EditgroupComponent } from './group/editgroup/editgroup.component';



@NgModule({
  declarations: [
    GroupComponent,
    ExpenseComponent,
    TransactionComponent,
    HomeComponent,
    FriendComponent,
    DashboardComponent,
    AddgroupComponent,
    EditgroupComponent],
  imports: [
    CommonModule,
    SharedModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forChild([
      { path: 'dashboard', component: DashboardComponent },
      { path: '', redirectTo: 'dashboard' },
      { path: 'friend', component: FriendComponent },
      { path: 'expense', component: ExpenseComponent },
      { path: 'group/:groupId', component: GroupComponent },
      { path: 'addgroup', component: AddgroupComponent },
      { path: 'editgroup/:groupId', component: EditgroupComponent },
      { path: 'transaction', component: TransactionComponent }
    ])
  ]
})
export class HomeModule { }
