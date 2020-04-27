import { Component, OnInit, Input } from '@angular/core';
import { UserService } from 'src/app/_service/user.service';
import { AuthService } from 'src/app/_service/auth.service';
import { AlertifyService } from 'src/app/_service/alertify.service';
import { Message } from 'src/app/_models/message';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {

  constructor(private userService:UserService, private authService:AuthService, private alertify: AlertifyService) { }
@Input() recipientId: number;
messages: Message[];

  ngOnInit() {
    this.loadMessages();
  }

  loadMessages(){
    this.userService.getMessageThread(this.authService.decodedToken.nameid[0],this.recipientId)
    .subscribe(msgs =>{
    this.messages = msgs;
    console.log(this.messages);
    },error => {
      this.alertify.error(error);
    })
  }
}
