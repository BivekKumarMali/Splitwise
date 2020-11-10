import { Component, OnInit } from '@angular/core';
import { FormBuilder, NgForm } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FriendDTO, GroupDTO, GroupsClient, MemberDTO, MembersClient, UsersClient } from 'src/app/core/api/splitwiseAPI';
import { UtilService } from 'src/app/core/util/util.service';

@Component({
  selector: 'app-editgroup',
  templateUrl: './editgroup.component.html',
  styleUrls: ['./editgroup.component.css']
})
export class EditgroupComponent implements OnInit {

  GroupName = '';
  Members: MemberDTO[];
  ShowAddMembers = false;
  Friends: FriendDTO[];
  filterFriends: FriendDTO[] = [];
  MemberUserId: string[] = [];
  constructor(
    private groupService: GroupsClient,
    private memberService: MembersClient,
    private userService: UsersClient,
    private utilService: UtilService,
    private route: ActivatedRoute,
    private router: Router,
    private formBuilder: FormBuilder
  ) { }

  ngOnInit(): void {
    const groupId = + this.route.snapshot.paramMap.get('groupId');
    this.fetchGroup(groupId);
  }
  fetchGroup(groupId: number) {
    this.groupService.getGroupById(groupId).subscribe({
      next: data => this.GroupName = data.groupName,
      error: err => this.router.navigate(['/error']),
      complete: () => this.CheckGroup(groupId)
    });
  }
  CheckGroup(groupId: number): void {
    if (this.GroupName !== '') {
      this.fetchMembers(groupId);
    }
  }
  fetchMembers(groupId: number) {
    this.memberService.getMembers(groupId).subscribe({
      next: data => this.Members = data,
      error: err => console.log(err),
      complete: () => this.fetchFriends()

    });
  }

  fetchFriends() {
    const userId = this.utilService.GetUserID();
    this.userService.getFriends(userId).subscribe({
      next: data => this.Friends = data,
    });

  }
  SetupFriendsList(): void {
    // tslint:disable-next-line:prefer-for-of
    for (let i = 0; i < this.Friends.length; i++) {
      const element = this.Friends[i];
      const index = this.Members.findIndex(x => x.id === element.id);
      if (index === -1) {
        this.filterFriends.push(element);
      }
    }
    this.ShowAddMembers = true;
  }
  AddUserID(event: any, i: number) {
    if (event.target.checked) {
      this.MemberUserId.push(this.Friends[i].id);
    }
    else {
      const index = this.MemberUserId.indexOf(this.Friends[i].id);
      this.MemberUserId.splice(index, 1);
    }

  }
  DeleteMember(id: number) {
    if (confirm('This result in deleteing every detail on thsi member in this group')) {
      console.log(id);

      this.memberService.deleteMember(id).subscribe({
        complete: () => this.ngOnInit()
      });
    }
    else {
      alert('notworking');
    }
  }
  EditGroup(form: NgForm) {
    const groupid = + this.route.snapshot.paramMap.get('groupId');
    const group = this.formBuilder.group({
      id: groupid,
      groupName: form.value.groupName,
      userId: this.utilService.GetUserID()
    });
    this.groupService.editGroup(group.value).subscribe({
      complete: () => this.AddMembers(groupid)
    });
  }
  AddMembers(groupid: number): void {
    if (this.MemberUserId.length !== 0) {
      const Members = this.formBuilder.array([]);

      this.MemberUserId.forEach(element => {
        Members.push(this.formBuilder.group({
          userId: element,
          groupId: groupid
        }));
      }),
        this.memberService.addMemberInBulk(Members.value).subscribe({
          error: err => console.log(err),
          complete: () => this.router.navigate(['/home/group', groupid])
        });
    }
  }
}
