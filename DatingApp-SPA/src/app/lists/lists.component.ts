import { Component, OnInit } from '@angular/core';
import { User } from '../_models/user';
import { Pagination, PaginatedResult } from '../_models/pagination';
import { AuthService } from '../_service/auth.service';
import { UserService } from '../_service/user.service';
import {  ActivatedRoute } from '@angular/router';
import { AlertifyService } from '../_service/alertify.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {

  users: User[];
  pagination: Pagination;
  likesParam: string;
  constructor( private authService: AuthService, private userService: UserService,
               private alertify: AlertifyService , private route: ActivatedRoute) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.users = data['users'].result;
      this.pagination = data['users'].pagination;
      });
    this.likesParam = 'Likers';
  }

  pageChanged(event: any): void {
    this.pagination.currentPage = event.page;
    this.loadUsers();
    // console.log(this.pagination.currentPage);
  }
  loadUsers() {
    console.log(this.likesParam);
    this.userService.getUsers(this.pagination.currentPage, this.pagination.itemsPerPage, null, this.likesParam).subscribe(
          (users: PaginatedResult<User[]>) => {
      this.users = users.result;
      this.pagination = users.pagination;
      // this.alertify.success('Users Loaded Susccefully');

    }, error => {
      this.alertify.error(error);
    }
    );
  }

}
