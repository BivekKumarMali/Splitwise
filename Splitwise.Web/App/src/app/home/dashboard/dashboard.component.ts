import { Component, OnInit } from '@angular/core';
import { combineAll } from 'rxjs/operators';
import { FriendDTO, SettlementDTO, SettlementsClient, UsersClient } from 'src/app/core/api/splitwiseAPI';
import { UtilService } from 'src/app/core/util/util.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  Friends: FriendDTO[];
  filterFriends: FriendDTO[] = [];
  Settlement: SettlementDTO[];
  Balance = 0;
  AmountOwe = 0;
  AmountOwed = 0;
  constructor(
    private userService: UsersClient,
    private utilService: UtilService,
    private settlementService: SettlementsClient
  ) { }

  ngOnInit(): void {
    const userid = this.utilService.GetUserID();
    this.fetchFriend(userid);
  }
  fetchFriend(id: string) {
    this.userService.getFriendWithBalance(id).subscribe({
      next: data => this.Friends = data,
      error: err => console.log(err),
      complete: () => this.SetupDashboard(id)
    });
  }
  SetupDashboard(id: string): void {
    this.Friends.forEach(element => {
      if (element.amount > 0) {
        this.AmountOwe += element.amount;
        this.PushToFilter(element);
      }
      else if (element.amount < 0) {
        this.AmountOwed += element.amount;
        this.PushToFilter(element);
      }
      this.Balance += element.amount;
    });
  }
  PushToFilter(element: FriendDTO) {
    const elementIndex = this.filterFriends.findIndex(x => x.id === element.id);
    if (elementIndex >= 0) {
      this.filterFriends[elementIndex].amount += element.amount;
    }
    else {
      this.filterFriends.push(element);
    }

  }

}
