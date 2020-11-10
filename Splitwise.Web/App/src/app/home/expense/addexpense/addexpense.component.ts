import { Component, OnInit } from '@angular/core';
import { FormBuilder, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { element } from 'protractor';
import { ExpensesClient, GroupsClient, MemberDTO, MembersClient } from 'src/app/core/api/splitwiseAPI';
import { UtilService } from 'src/app/core/util/util.service';

@Component({
  selector: 'app-addexpense',
  templateUrl: './addexpense.component.html',
  styleUrls: ['./addexpense.component.css']
})
export class AddexpenseComponent implements OnInit {

  GroupName = '';
  Members: MemberDTO[];
  ExpenseDetails: any[] = [];
  IsMultiplePeople = false;
  ShowPercentage = false;
  ShowCustom = false;
  ShowMessage: string;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private utilService: UtilService,
    private groupService: GroupsClient,
    private expenseService: ExpensesClient,
    private memberService: MembersClient
  ) { }

  ngOnInit(): void {
    const groupId = + this.route.snapshot.paramMap.get('groupId');
    this.fetchGroupDetail(groupId);
    this.fetchMember(groupId);
  }
  fetchMember(groupId: number) {
    this.memberService.getMemberWithBalance(groupId).subscribe({
      next: data => this.Members = data,
      error: err => console.log(err)
    });
  }
  fetchGroupDetail(groupId: number) {
    this.groupService.getGroupById(groupId).subscribe({
      next: data => this.GroupName = data.groupName,
      error: err => console.log(err)
    });
  }

  SetUpExpense(form: NgForm) {
    const amountPaid = this.ExpenseDetails.reduce((sum: number, b: any) => sum + Number(b.amountPaid), 0);
    const amountOwe = this.ExpenseDetails.reduce((sum: number, b: any) => sum + Number(b.amountOwe), 0);
    if (this.ShowCustom) {
      if (amountOwe === amountPaid) {
        this.AddExpense(form);
      }
      else {
        this.ShowMessage = 'Amount Paid is not equal to Amount Owe<br>Please Check';
      }
    }
    else if (this.ShowPercentage) {
      if (amountOwe !== 100) {
        // tslint:disable-next-line:no-shadowed-variable
        this.ExpenseDetails.forEach(element => {
          element.amountOwe = amountPaid * element.amountOwe / 100;
        });
        this.AddExpense(form);
      }
      else {
        this.ShowMessage = 'Total Percentage not 100';
      }
    }
    else {
      const amount = amountPaid / this.Members.length;
      // tslint:disable-next-line:no-shadowed-variable
      this.ExpenseDetails.forEach(element => {
        element.amountOwe = amount;
      });
      this.AddToBePaidByEqually(amount);
      this.AddExpense(form);
    }

  }
  AddExpense(form: NgForm) {
    const userid = this.utilService.GetUserID();
    const groupid = + this.route.snapshot.paramMap.get('groupId');
    let expenseId = 0;
    const expense = this.formBuilder.group({
      expenseName: form.value.name,
      timeStamp: new Date(),
      userId: userid,
      groupId: groupid
    });
    this.expenseService.addExpense(expense.value).subscribe({
      next: data => expenseId = data,
      complete: () => this.AddExpenseDetail(expenseId, groupid)
    });
  }
  AddExpenseDetail(expenseId: number, groupid: number) {
    // tslint:disable-next-line:no-shadowed-variable
    this.ExpenseDetails.forEach(element => {
      element.expenseId = expenseId;
    });
    const expenseDetail = this.formBuilder.array(this.ExpenseDetails);
    this.expenseService.addExpenseDetails(expenseDetail.value).subscribe({
      error: err => console.log(err),
      complete: () => this.router.navigate(['/home/group', groupid])

    });
  }


  ActivateMultiplePeople(event: any) {
    this.IsMultiplePeople = event.target.checked;
  }
  AddSingleToPaidBy(form: NgForm, event: Event) {
    this.AddToPaidBy(form.value.paidBy, event);
  }
  AddToPaidBy(userid: string, event: any) {
    if (!this.ExpenseDetailExist(userid)) {
      if (event.target.value) {
        const expenseDetail = {
          amountPaid: Number(event.target.value),
          userId: userid,
          amountOwe: null,
          expenseId: null
        };
        this.ExpenseDetails.push(expenseDetail);
      }
      else {
        const index = this.ExpenseDetails.findIndex(x => x.userId === userid);
        this.ExpenseDetails = this.ExpenseDetails.splice(index, 1);
      }
    }
    else {
      const expenseDetail = this.ExpenseDetails.find(x => x.userId === userid);
      expenseDetail.amountPaid = event.target.value;
    }
    console.log(this.ExpenseDetails);

  }


  PayeeList(event) {
    if (event.target.id === 'percentage') {
      this.ShowPercentage = true;
      this.ShowCustom = false;
    }
    else if (event.target.id === 'custom') {
      this.ShowPercentage = false;
      this.ShowCustom = true;
    }
    else {
      this.ShowPercentage = false;
      this.ShowCustom = false;
    }
  }
  AddToBePaidByPercentage(userid: string, event: any) {
    if (this.ExpenseDetailExist(userid)) {
      const expenseDetail = this.ExpenseDetails.find(x => x.userId === userid);
      expenseDetail.amountOwe = event.target.value;
    }
    else {
      const expenseDetail = {
        amountPaid: 0,
        userId: userid,
        amountOwe: event.target.value,
        expenseId: null
      };
      this.ExpenseDetails.push(expenseDetail);
    }
    console.log(this.ExpenseDetails);
  }
  AddToBePaidByCustom(userid: string, event: any) {
    if (this.ExpenseDetailExist(userid)) {
      const expenseDetail = this.ExpenseDetails.find(x => x.userId === userid);
      expenseDetail.amountOwe = Number(event.target.value);
    }
    else {
      const expenseDetail = {
        amountPaid: 0,
        userId: userid,
        amountOwe: Number(event.target.value),
        expenseId: null
      };
      this.ExpenseDetails.push(expenseDetail);
    }
    console.log(this.ExpenseDetails);
  }

  AddToBePaidByEqually(amount: number) {
    this.Members.forEach(member => {
      if (!this.ExpenseDetailExist(member.id)) {
        const expenseDetail = {
          amountPaid: 0,
          userId: member.id,
          amountOwe: amount,
          expenseId: null
        };
        this.ExpenseDetails.push(expenseDetail);
      }
    });
  }

  ExpenseDetailExist(userId: string): boolean {
    if (this.ExpenseDetails.length === 0) {
      return false;
    }
    else {
      return this.ExpenseDetails.findIndex(x => x.userId === userId) !== -1;
    }
  }
}
