import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { User } from 'src/app/_models/user';
import { AlertifyService } from 'src/app/_service/alertify.service';
import { NgForm } from '@angular/forms';
import { AuthService } from 'src/app/_service/auth.service';
import { UserService } from 'src/app/_service/user.service';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
@ViewChild('editForm', {static: false}) editForm: NgForm;
  user: User;
  //#region Check on Browser if any changes and unexpected/close the browser
  @HostListener('window:beforeunload', ['$event'])
  unloadNotification($event: any) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }
  //#endregion
  constructor( private route: ActivatedRoute, private alertify: AlertifyService,
               private userService: UserService, private authService: AuthService ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data ['user'];

    });
  }

  updateUser() {
    // console.log(this.user);
    // console.info(this.authService.decodedToken.nameid[0]);
    this.userService.updateUser(this.authService.decodedToken.nameid[0] , this.user).subscribe( next => {
        this.alertify.success('Profile Update Successfully');
        this.editForm.reset(this.user);
      }, error => {
        this.alertify.error(error);
      });
      }
   }
