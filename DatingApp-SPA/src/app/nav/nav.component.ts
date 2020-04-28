import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';
import { AlertifyService } from '../_service/alertify.service';
import { Routes, Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {


  // model: any;

  model: any = {};
  photoUrl: string;
  /*model = {
    var  username,
    var password
  };
  */
  // model = {}; // not use any but {} object
  // techSpecMeta: {};
  // Type script this means to declare a property of type {} with no value initialized. It is the same as:
  // techSpecMeta: Object
  constructor(private authService: AuthService, private alertify: AlertifyService, private router: Router) { }

  ngOnInit() {
    this.authService.currentPhotoUrl.subscribe(photoUrl => this.photoUrl = photoUrl);
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged In Successfully');
      // this.router.navigate(['/members']);
    }, error => {
      this.alertify.error(error);
      this.router.navigate(['/home']);
    }, () => {
      this.router.navigate(['/members']);
    }
    );
  }
  loggedIn() {
    // const token = localStorage.getItem('token');
    // return !!token; // Short hand !! boloean true/false
    return this.authService.loggedIn();
  }

  logout() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    this.authService.decodedToken = null;
    this.authService.currentUser = null;
    this.alertify.message('Logged out Succesfully');
    this.router.navigate(['/home']);
  }

}


