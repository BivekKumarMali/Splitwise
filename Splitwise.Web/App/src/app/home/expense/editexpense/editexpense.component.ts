import { Component, OnInit } from '@angular/core';
import { FormBuilder, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ExpenseDetailDTO, ExpenseDTO, ExpensesClient, GroupsClient } from 'src/app/core/api/splitwiseAPI';
import { UtilService } from 'src/app/core/util/util.service';

@Component({
  selector: 'app-editexpense',
  templateUrl: './editexpense.component.html',
  styleUrls: ['./editexpense.component.css']
})
export class EditexpenseComponent implements OnInit {

  GroupName = '';
  ExpenseName = '';
  ExpenseDetails: ExpenseDetailDTO[];
  Expense: ExpenseDTO;
  constructor(
    private route: ActivatedRoute,
    private expenseService: ExpensesClient,
    private utilService: UtilService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  ngOnInit(): void {
    const expenseId = + this.route.snapshot.paramMap.get('expenseid');
    this.fetchExpense(expenseId);
    this.fetchExpenseDetail(expenseId);
  }
  fetchExpenseDetail(expenseId: number) {
    this.expenseService.getExpenseDetail(expenseId).subscribe({
      next: data => this.ExpenseDetails = data,
      error: err => console.log(err)

    });
  }
  fetchExpense(expenseId: number) {
    this.expenseService.getExpenseByID(expenseId).subscribe({
      next: data => {
        this.Expense = data,
          this.ExpenseName = data.expenseName;
      },
      error: err => console.log(err)
    });
  }
  SetUpExpense() {
    this.Expense.expenseName = this.ExpenseName;
    const expense = this.formBuilder.group({
      id: this.Expense.id,
      expenseName: this.ExpenseName,
      timeStamp: new Date(),
      userId: this.utilService.GetUserID(),
      groupId: this.Expense.groupId
    });
    this.expenseService.editExpense(expense.value).subscribe({
      next: () => this.router.navigate(['/home'])
    });

  }

}
