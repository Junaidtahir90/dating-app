import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_service/auth.service';
import { AlertifyService } from '../_service/alertify.service';

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
  constructor( private authService: AuthService, private alertify: AlertifyService ) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(next => {
      this.alertify.success('Logged In Succesfullys');
    }, error => {
      this.alertify.error(error);
    }
    );
  }
  
    loggedIn() {
      //const token = localStorage.getItem('token');
      //return !!token; // Short hand !! boloean true/false
      return this.authService.loggedIn();
      
    }

    logout() {
      localStorage.removeItem('token');
      this.alertify.message('Logged Out Succesfully');
    }

  }


