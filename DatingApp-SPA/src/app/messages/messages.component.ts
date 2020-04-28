import { Component, OnInit } from '@angular/core';
import { Message } from '../_models/message';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { UserService } from '../_service/user.service';
import { AuthService } from '../_service/auth.service';
import { AlertifyService } from '../_service/alertify.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messages: Message[];
  pagination: Pagination;
  messageContainer = 'Unread';
  loggedUserId: number;

  constructor(private userService: UserService, private authService: AuthService,
              private alertify: AlertifyService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.messages = data.messages.result;
      this.pagination = data.messages.pagination;
    });
    this.getCurrentUserId();

  }

  loadMessages() {
    this.getCurrentUserId();
    this.userService.getMessages(this.loggedUserId, this.pagination.currentPage,
      this.pagination.itemsPerPage, this.messageContainer)
      .subscribe((res: PaginatedResult<Message[]>) => {
        this.messages = res.result;
        this.pagination = res.pagination;
      }, error => {
        this.alertify.error(error);
      });

  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadMessages();
    // console.log(this.pagination.currentPage);
  }

  getCurrentUserId() {
    this.loggedUserId = this.authService.decodedToken.nameid[0];
    return this.loggedUserId;
  }

  deleteMessage(messageId: number) {
    // this.messageId=messageId;
    this.getCurrentUserId();
    console.log(messageId);
    this.alertify.confirm('Are you sure you want to delete this message?', () => {
      this.userService.deleteMessage(messageId, this.loggedUserId).subscribe(data => {
       // debugger;
        // console.log(this.loggedUserId);
        this.messages.splice(this.messages.findIndex(m => m.id === messageId), 1);
        this.alertify.success('Message has been deleted successfully');
      }, error => {
       // console.log(error);
        this.alertify.error(error);
      });
    });
  }
}
