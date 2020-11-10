import { Component, OnInit } from '@angular/core';
import { FormBuilder, NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { FriendDTO, GroupsClient, Member, MembersClient, UsersClient } from 'src/app/core/api/splitwiseAPI';
import { UtilService } from 'src/app/core/util/util.service';

@Component({
  selector: 'app-addgroup',
  templateUrl: './addgroup.component.html',
  styleUrls: ['./addgroup.component.css']
})
export class AddgroupComponent implements OnInit {

  Friends: FriendDTO[];
  MemberUserId: string[] = [this.utilService.GetUserID()];
  userId: string[];
  ShowAddMembers = false;


  constructor(
    private formBuilder: FormBuilder,
    private userService: UsersClient,
    private groupService: GroupsClient,
    private memberService: MembersClient,
    private utilService: UtilService,
    private router: Router
  ) { }

  ngOnInit(): void {
  }
  fetchFriends() {
    const userId = this.utilService.GetUserID();
    this.userService.getFriends(userId).subscribe({
      next: data => this.Friends = data,
      complete: () => this.ShowAddMembers = true
    });
  }
  AddGroup(form: NgForm) {
    let id = 0;
    const group = this.formBuilder.group({
      groupName: form.value.groupName,
      userId: this.utilService.GetUserID()
    });
    this.groupService.addGroup(group.value).subscribe({
      next: data => id = data,
      complete: () => this.AddMembers(id)
    });
  }

  AddMembers(groupid: number) {
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

  AddUserID(event: any, i: number) {
    if (event.target.checked) {
      this.MemberUserId.push(this.Friends[i].id);
    }
    else {
      const index = this.MemberUserId.indexOf(this.Friends[i].id);
      this.MemberUserId.splice(index, 1);
    }
  }
}
