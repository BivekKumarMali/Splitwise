import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { GroupComponent } from './group/group.component';
import { ExpenseComponent } from './expense/expense.component';
import { TransactionComponent } from './transaction/transaction.component';
import { HomeComponent } from './home.component';
import { FriendComponent } from './friend/friend.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [GroupComponent, ExpenseComponent, TransactionComponent, HomeComponent, FriendComponent],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule.forChild([
      { path: 'friend', component: FriendComponent },
      { path: 'expense', component: ExpenseComponent },
      { path: 'group/:groupId', component: GroupComponent },
      { path: 'transaction', component: TransactionComponent }
    ])
  ]
})
export class HomeModule { }
