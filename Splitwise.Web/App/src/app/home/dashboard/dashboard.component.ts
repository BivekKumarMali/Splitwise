import { Component, OnInit } from '@angular/core';
import { FriendDTO, UsersClient } from 'src/app/core/api/splitwiseAPI';
import { UtilService } from 'src/app/core/util/util.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  Friends: FriendDTO[];
  constructor(
    private userService: UsersClient,
    private utilService: UtilService
  ) { }

  ngOnInit(): void {
    const userid = this.utilService.GetUserID();
    this.fetchFriend(userid);
  }
  fetchFriend(id: string) {
    this.userService.getFriendWithBalance(id).subscribe({
      next: data => this.Friends = data,
      error: err => console.log(err),
    });
  }

}
