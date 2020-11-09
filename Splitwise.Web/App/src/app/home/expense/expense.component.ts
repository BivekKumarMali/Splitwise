import { Component, OnInit } from '@angular/core';
import { ExpenseDTO, ExpensesClient } from 'src/app/core/api/splitwiseAPI';
import { UtilService } from 'src/app/core/util/util.service';

@Component({
  selector: 'app-expense',
  templateUrl: './expense.component.html',
  styleUrls: ['./expense.component.css']
})
export class ExpenseComponent implements OnInit {

  Expenses: ExpenseDTO[];
  constructor(
    private utilService: UtilService,
    private expenseService: ExpensesClient
  ) { }

  ngOnInit(): void {
    const userid = this.utilService.GetUserID();
    this.expenseService.getExpenseByUserID(userid).subscribe({
      next: data => this.Expenses = data,
      error: err => console.log(err)
    });
  }

  DeleteExpense(id: number) { }

}
