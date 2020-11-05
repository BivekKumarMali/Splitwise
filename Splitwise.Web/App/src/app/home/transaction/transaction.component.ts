import { Component, OnInit } from '@angular/core';
import { SettlementDTO } from 'src/app/model/models';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {

  Settlements: SettlementDTO[];
  constructor() { }

  ngOnInit(): void {
  }
  DeleteSettlement(id: number) { }

}
