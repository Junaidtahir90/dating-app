import { Component, OnInit, Input } from '@angular/core';
import { UserService } from 'src/app/_service/user.service';
import { AuthService } from 'src/app/_service/auth.service';
import { AlertifyService } from 'src/app/_service/alertify.service';
import { Message } from 'src/app/_models/message';
import { tap } from 'rxjs/operators';

@Component({
  selector: 'app-member-messages',
  templateUrl: './member-messages.component.html',
  styleUrls: ['./member-messages.component.css']
})
export class MemberMessagesComponent implements OnInit {

  constructor(private userService: UserService, private authService: AuthService, private alertify: AlertifyService) { }
  @Input() recipientId: number;
  messages: Message[];
  newMessage: any = {};
  loggedUserId: number;
  messageId: number;

  ngOnInit() {
    this.loadMessages();
    this.getCurrentUserId();
  }

  loadMessages() {
    this.getCurrentUserId();
    this.userService.getMessageThread(this.loggedUserId, this.recipientId)
    .pipe(
      tap( messages => {
        // tslint:disable-next-line: prefer-for-of
        for (let i = 0; i < messages.length; i++) {
          if ((messages[i].isRead === false) && (messages[i].recipientId === +this.loggedUserId )) {
              this.userService.markAsReadMessage(this.loggedUserId, messages[i].id);
          }
        }
      }
      )
    )
      .subscribe(msgs => {
        this.messages = msgs;
        console.log(this.messages);
      }, error => {
        this.alertify.error(error);
      });
  }
  sendMessage() {
    this.newMessage.recipientId = this.recipientId;
    this.userService.sendMessage(this.loggedUserId, this.newMessage)
      .subscribe((message: Message) => {
      // debugger;
      this.messages.unshift(message);

      console.log(this.messages);
      this.newMessage.content = '';

      }, error => {
        this.alertify.error(error);
      });

  }

  markAsReadMessage() {

  }
  getCurrentUserId() {
    this.loggedUserId = this.authService.decodedToken.nameid[0];
    return this.loggedUserId;
  }

}
