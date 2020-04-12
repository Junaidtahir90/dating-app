import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {


 // model: any;
 model = {};
  /*model = {
    var  username,
    var password
  };
  */
 // model = {}; // not use any but {} object
  // techSpecMeta: {};
  // Type script this means to declare a property of type {} with no value initialized. It is the same as:
  // techSpecMeta: Object
  constructor( private authService: AuthService ) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      console.log('Logged In Succesfully');
    }, error => {
      console.log('Failed to Log In');
    }
    );
  }
    loggedIn() {
      const token=localStorage.getItem('token');
      return !!token; // Short hand !! boloean true/false
    }

    logout() {
      localStorage.removeItem('token');
      console.log('Logged Out Succesfully');
    }

  }


