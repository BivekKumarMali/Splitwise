import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { GroupDTO, GroupsClient, MemberDTO, MembersClient } from 'src/app/core/api/splitwiseAPI';

@Component({
  selector: 'app-editgroup',
  templateUrl: './editgroup.component.html',
  styleUrls: ['./editgroup.component.css']
})
export class EditgroupComponent implements OnInit {

  GroupName = '';
  Members: MemberDTO[];
  constructor(
    private groupService: GroupsClient,
    private memberService: MembersClient,
    private route: ActivatedRoute,
    private router: Router
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
    this.memberService.getMemberWithBalance(groupId).subscribe({
      next: data => this.Members = data,
      error: err => console.log(err)

    });
  }
  EditGroup() { }

}
