import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { SettlementDTO, SettlementsClient } from 'src/app/core/api/splitwiseAPI';
import { UtilService } from 'src/app/core/util/util.service';

@Component({
  selector: 'app-transaction',
  templateUrl: './transaction.component.html',
  styleUrls: ['./transaction.component.css']
})
export class TransactionComponent implements OnInit {

  Settlements: SettlementDTO[];
  constructor(
    private settlementService: SettlementsClient,
    private utilService: UtilService
  ) { }

  ngOnInit(): void {
    const userId = this.utilService.GetUserID();
    this.fetchSettlementByUserId(userId);
  }

  fetchSettlementByUserId(userId: string) {
    this.settlementService.getSettlementByUserID(userId).subscribe({
      next: data => this.Settlements = data
    });
  }
  DeleteSettlement(id: number) {
    if (confirm('Are You Sure?')) {
      this.settlementService.deleteSettlement(id).subscribe({
        complete: () => this.ngOnInit()
      });
    }
  }

}
