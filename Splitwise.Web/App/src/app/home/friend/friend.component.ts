import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {
  MemberDTO,
  SettlementDTO,
  ExpenseDTO,
  FriendDTO,
  GroupDTO,
  ExpensesClient,
  GroupsClient,
  MembersClient,
  SettlementsClient,
  UsersClient
} from 'src/app/core/api/splitwiseAPI';
import { UtilService } from 'src/app/core/util/util.service';

@Component({
  selector: 'app-friend',
  templateUrl: './friend.component.html',
  styleUrls: ['./friend.component.css']
})
export class FriendComponent implements OnInit {


  Settlements: SettlementDTO[];
  Expenses: ExpenseDTO[];
  Friends: FriendDTO[];
  Group: GroupDTO;
  Friend: FriendDTO;
  activateSettlement: string;
  Name: string;
  Amount: number;

  constructor(
    private memberService: MembersClient,
    private groupService: GroupsClient,
    private settlementService: SettlementsClient,
    private expenseService: ExpensesClient,
    private userService: UsersClient,
    private formBuilder: FormBuilder,
    private utilService: UtilService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {

    this.route.params.subscribe(params => {
      const userid = this.utilService.GetUserID();
      const friendid = this.route.snapshot.paramMap.get('id');
      this.fetchFriend(userid, friendid);
      this.fetchExpense(userid, friendid);
      this.fetchSettlement(userid, friendid);
    });
  }
  fetchSettlement(userid: string, friendid: string) {
    const ufid = userid + ' ' + friendid;
    this.settlementService.getSettlementByFriend(ufid).subscribe({
      next: data => this.Settlements = data,
      error: err => console.log(err)
    });
  }
  fetchExpense(userid: string, friendid: string) {
    const ufid = userid + ' ' + friendid;
    this.expenseService.getExpenseByFriend(ufid).subscribe({
      next: data => this.Expenses = data,
      error: err => console.log(err)
    });
  }
  fetchFriend(id: string, friendid: string) {
    this.userService.getFriendWithBalance(id).subscribe({
      next: data => this.Friends = data,
      error: err => console.log(err),
      complete: () => {
        const friend = this.Friends.find(x => x.id === friendid);
        this.Name = friend.name;
        this.Amount = friend.amount;
      }
    });
  }


  DeleteSettlement(id: number) {
    if (confirm('Are You Sure?')) {
      this.settlementService.deleteSettlement(id).subscribe({
        complete: () => this.ngOnInit()
      });
    }
  }

  displaySettlementFrom() {
    this.activateSettlement = this.activateSettlement ? '' : 'activate';
  }

  RouteToExpenseFrom(id?: number) {
    this.router.navigate(['/home/addexpense', 0]);
  }

  AddSettlement(form: NgForm) {
    const settlement = this.formBuilder.group({
      amount: form.value.amount,
      timeStamp: new Date(),
      payUserId: form.value.payUserID,
      payeeUserId: form.value.payeeUserID,
      groupId: this.Group.id
    });

    this.settlementService.addSettlement(settlement.value).subscribe({
      error: err => console.log(err),
      complete: () => {
        this.ngOnInit();
        this.displaySettlementFrom();
      }
    });
  }
}
