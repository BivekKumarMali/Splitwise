import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ExpenseDetailDTO, ExpensesClient } from 'src/app/core/api/splitwiseAPI';

@Component({
  selector: 'app-expensedetail',
  templateUrl: './expensedetail.component.html',
  styleUrls: ['./expensedetail.component.css']
})
export class ExpensedetailComponent implements OnInit {

  ExpenseDetails: ExpenseDetailDTO[];
  constructor(
    private route: ActivatedRoute,
    private expenseService: ExpensesClient
  ) { }

  ngOnInit(): void {
    const id = + this.route.snapshot.paramMap.get('expenseid');
    this.fetchExpenseDetail(id);
  }
  fetchExpenseDetail(id: number) {
    this.expenseService.getExpenseDetail(id).subscribe({
      next: data => this.ExpenseDetails = data,
      error: err => console.log(err)
    });
  }

}
