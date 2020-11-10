import { Component, OnInit } from '@angular/core';
import { FormBuilder, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import {
  MemberDTO,
  SettlementDTO,
  ExpenseDTO,
  FriendDTO,
  MembersClient,
  GroupsClient,
  GroupDTO,
  SettlementsClient,
  UsersClient,
  ExpensesClient
} from 'src/app/core/api/splitwiseAPI';
import { UtilService } from 'src/app/core/util/util.service';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.css']
})
export class GroupComponent implements OnInit {

  Members: MemberDTO[];
  Settlements: SettlementDTO[];
  Expenses: ExpenseDTO[];
  MemberWithBalance: MemberDTO[];
  Friends: FriendDTO[];
  Group: GroupDTO;
  GroupId: number;
  activateSettlement: string;
  activateExpense: string;
  activateMember: string;
  ShowMemberError: boolean;
  AddMemberError: any;
  red = 'red';
  green = 'green';

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
      const userId = this.utilService.GetUserID();
      this.GroupId = +this.route.snapshot.paramMap.get('groupId');
      this.fetchGroup(this.GroupId);
      this.fetchMemberWithBalance(this.GroupId);
      this.fetchSettlement(this.GroupId);
      this.fetchfriends(userId);
      this.fetchExpense(this.GroupId);
    });

  }
  fetchExpense(groupid: number) {
    this.expenseService.getExpenseByGroupID(groupid).subscribe({
      next: data => this.Expenses = data,
      error: err => console.log(err)
    });
  }
  fetchfriends(userId: string) {
    this.userService.getFriends(userId).subscribe({
      next: data => this.Friends = data,
      error: err => console.log(err)
    });
  }
  fetchSettlement(groupid: number) {
    this.settlementService.getSettlementByGroupId(groupid).subscribe({
      next: data => this.Settlements = data,
      error: err => console.log(err)
    });
  }

  fetchGroup(groupid: number) {
    this.groupService.getGroupById(groupid).subscribe({
      next: data => this.Group = data,
      error: err => console.log(err)
    });
  }
  fetchMemberWithBalance(groupid: number) {
    this.memberService.getMemberWithBalance(groupid).subscribe({
      next: data => this.MemberWithBalance = data,
      error: err => console.log(err)
    });
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

  AddMember(form: NgForm) {
    const member = this.formBuilder.group({
      userId: form.value.friend,
      groupId: this.Group.id
    });
    this.memberService.addMember(member.value).subscribe({
      error: err => this.AddMemberError = err,
      complete: () => {
        this.ngOnInit();
        this.displayMemberFrom();
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
  DeleteExpense(id: number) {
    if (confirm('Are You Sure?')) {
      this.expenseService.deleteExpense(id).subscribe({
        complete: () => this.ngOnInit()
      });
    }
  }



  displaySettlementFrom() {
    this.activateSettlement = this.activateSettlement ? '' : 'activate';
  }

  RouteToExpenseFrom(id?: number) {
    const groupId = this.route.snapshot.paramMap.get('groupId');
    this.router.navigate(['/home/addexpense', groupId]);
  }
  displayMemberFrom() {
    this.activateMember = this.activateMember ? '' : 'activate';
  }

}
