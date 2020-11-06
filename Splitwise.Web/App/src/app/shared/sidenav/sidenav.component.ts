import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UsersService } from 'src/app/core/api/setup/api';
import { UtilService } from 'src/app/core/util/util.service';
import { FriendDTO, Group, GroupDTO, User } from 'src/app/model/models';
import { UserDTO } from 'src/app/model/userDTO';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent implements OnInit {

  pagetitle = 'Add';
  searchResult: UserDTO[];
  Friends: FriendDTO[];
  group: Group = { groupName: '' };
  groupNames: GroupDTO[];
  errorMessage: string;
  activeGroup: string;
  activeFriend: string;
  constructor(
    private userService: UsersService,
    private utilService: UtilService
  ) { }

  ngOnInit(): void {
    this.errorMessage = '';
    this.fetchFriends();
  }

  fetchFriends() {
    const userid = this.utilService.GetUserID();
    this.userService.getFriends(userid).subscribe({
      next: friends => this.Friends = friends,
      error: err => console.log(err)
    });
  }

  displayGroupFrom() {
    this.activeGroup = this.activeGroup ? '' : 'activate';
  }
  displayFriendFrom() {
    this.activeFriend = this.activeFriend ? '' : 'activate';
    this.errorMessage = '';
  }

  AddGroup(form: NgForm) {
    console.log(this.group);
    console.log(form.value);
  }

  SearchFriend(form: NgForm) {
    this.userService.ByMail(form.value.mail).subscribe({
      next: friends => this.SetUpSearchResult(friends)
    });
  }
  SetUpSearchResult(friends: UserDTO[]): void {
    this.searchResult = [];
    const userId = this.utilService.GetUserID();
    friends.forEach(element => {
      if (userId !== element.id) {
        this.searchResult.push(element);
      }
    });
  }
  AddFriend(id: string) {
    const friend = {
      id: 0,
      userId: this.utilService.GetUserID(),
      friendId: id
    };
    this.userService.addFriend(friend).subscribe({
      error: err => this.errorMessage = err,
      complete: () => this.ngOnInit()
    })
  }
}
