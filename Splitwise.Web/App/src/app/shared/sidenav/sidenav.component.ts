import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, NgForm } from '@angular/forms';
import { FriendDTO, Group, GroupDTO, GroupsClient, UserDTO, UsersClient } from 'src/app/core/api/splitwiseAPI';
import { UtilService } from 'src/app/core/util/util.service';

@Component({
  selector: 'app-sidenav',
  templateUrl: './sidenav.component.html',
  styleUrls: ['./sidenav.component.css']
})
export class SidenavComponent implements OnInit {

  pagetitle = 'Add';
  searchResult: UserDTO[];
  Friends: FriendDTO[];
  group: FormGroup = this.fb.group({ groupName: '' });
  groupNames: GroupDTO[];
  errorMessage: string;
  activeGroup: string;
  activeFriend: string;
  constructor(
    private userService: UsersClient,
    private groupService: GroupsClient,
    private utilService: UtilService,
    private fb: FormBuilder
  ) { }

  ngOnInit(): void {
    this.errorMessage = '';
    const userid = this.utilService.GetUserID();
    this.fetchFriends(userid);
    this.fetchGroups(userid);
  }
  fetchGroups(userid: string) {
    this.groupService.getGroups(userid).subscribe({
      next: group => this.groupNames = group,
      error: err => console.log(err)
    });
  }

  fetchFriends(userid: string) {
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

  SearchFriend(form: NgForm) {
    this.userService.usersByMail(form.value.mail).subscribe({
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
    const friend = this.fb.group({
      id: 0,
      userId: this.utilService.GetUserID(),
      friendId: id
    });
    this.userService.addFriend(friend.value).subscribe({
      error: err => this.errorMessage = err,
      complete: () => this.ngOnInit()
    });
  }
}
