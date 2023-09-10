import { Component, HostListener, OnInit, ViewChild } from '@angular/core';
import { NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { take } from 'rxjs';
import { Member } from 'src/app/_models/member';
import { User } from 'src/app/_models/user';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm: NgForm | undefined; //REFRESH FORM FROM ID editForm
  @HostListener('window:beforeunload', ['$event']) unloadNotifcation($event: any) {
    if (this.editForm?.dirty) {
      $event.returnValue = true;

    }
  } //PREVENT ANY EVENT FROM BROWSER LIKE REFRESH, BACK , ETC
  member: Member | undefined;
  user: User | null = null;

  //INJECT ACCOUNT SERVICE TO GET USERNAME / CURRENT USER / USER MODEL
  //INJECT MEMBER SERVICE TO GET MEMBER PROFILE
  constructor(private accountService: AccountService, private memberService: MembersService, private toastr: ToastrService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => this.user = user
    }) // SUBSCRIBE TO USER FROM ACCOUNT SERVICE USER
  }
  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    if (!this.user) return;
    this.memberService.getMember(this.user.username).subscribe({
      next: member => this.member = member
    })
  }

  updateMember() {
    this.memberService.updateMember(this.editForm?.value).subscribe({
      next: _ => {
        this.toastr.success('Profile Updated Successfully'); // _ UPDATE IS NO RESPONSE FROM API 
        this.editForm?.reset(this.member); //RESET FORM INJECT MEMBER DB MODEL
      }
    })
  }
}
