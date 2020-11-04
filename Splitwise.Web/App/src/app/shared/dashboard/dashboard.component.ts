import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { FriendDTO, Group, GroupDTO } from 'src/app/model/models';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  pagetitle = 'Add';
  searchResult: FriendDTO[];
  group: Group = { groupName: '' };
  groupNames: GroupDTO[];
  errorMessage: string;
  activeGroup: string;
  activeFriend: string;
  constructor() { }

  ngOnInit(): void {
  }



  displayGroupFrom() {
    this.activeGroup = this.activeGroup ? '' : 'activate';
  }
  displayFriendFrom() {
    this.activeFriend = this.activeFriend ? '' : 'activate';
  }

  AddGroup(form: NgForm) {
    console.log(this.group);
    console.log(form.value);


  }

  SearchFriend(form: NgForm) {
    console.log(form.value);
  }
  AddFriend(id: number) { }
}
