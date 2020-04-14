import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../_service/auth.service';
import { AlertifyService } from '../_service/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  @Input() valuesFromHome: any;
  @Output () cancelRegister = new EventEmitter();

  constructor(private http: HttpClient, private authService: AuthService, private alertify: AlertifyService ) { }

  ngOnInit() {
  }

  register() {

    this.authService.register(this.model).subscribe(next => {
      this.alertify.success('Register Succesfully');
    }, error => {
      this.alertify.error(error);
    });
   // return console.log(this.model);
  }

  cancel() {
    this.cancelRegister.emit(false); // any thing passed,which want to emit,bolean/object too
  }

}
