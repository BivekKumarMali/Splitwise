import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ExpenseDTO, FriendDTO, MemberDTO, SettlementDTO } from 'src/app/model/models';

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
  activateSettlement: string;
  activateExpense: string;
  activateMember: string;
  ShowMemberError: boolean;
  constructor() { }

  ngOnInit(): void {
  }

  displaySettlementFrom() {
    this.activateSettlement = this.activateSettlement ? '' : 'activate';
  }

  displayExpenseFrom(id?: number) {
    this.activateExpense = this.activateExpense ? '' : 'activate';
  }
  displayMemberFrom() {
    this.activateMember = this.activateMember ? '' : 'activate';
  }
  AddSettlement(form: NgForm) {
    console.log(form.value);
  }

  AddMember(form: NgForm) {
    console.log(form.value);
  }
  DeleteSettlement(id: number) { }

}
