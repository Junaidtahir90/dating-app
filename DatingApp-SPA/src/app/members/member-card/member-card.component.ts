import { Component, OnInit, Input } from '@angular/core';
import { User } from 'src/app/_models/user';
import { AuthService } from 'src/app/_service/auth.service';
import { UserService } from 'src/app/_service/user.service';
import { AlertifyService } from 'src/app/_service/alertify.service';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.css']
})
export class MemberCardComponent implements OnInit {

  @Input() user: User;
  constructor(private authService: AuthService, private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  likeUser(id: number) {
    this.userService.likeUser(this.authService.decodedToken.nameid[0], id).subscribe(data => {
      this.alertify.success('You have liked: ' + this.user.nickName);
    }, error => {
      this.alertify.error(error);
    });

  }
}
