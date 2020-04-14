import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../_service/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  @Input() valuesFromHome: any;
  @Output () cancelRegister = new EventEmitter();

  constructor(private http: HttpClient, private authService: AuthService ) { }

  ngOnInit() {
  }

  register() {

    this.authService.register(this.model).subscribe(next => {
      console.log('Register Succesfully');
    }, error => {
      console.log(error);
    });
   // return console.log(this.model);
  }

  cancel() {
    this.cancelRegister.emit(false); // any thing passed,which want to emit,bolean/object too
    return console.log('cancelled');
  }

}
