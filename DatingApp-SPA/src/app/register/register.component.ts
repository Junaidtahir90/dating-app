import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model : any ={};
  @Input() valuesFromHome : any; 
  @Output () cancelRegister = new EventEmitter();
 
  constructor(private http: HttpClient ) { }

  ngOnInit() {
  }

  register() {
    return console.log(this.model);
  }

  cancel() {
    this.cancelRegister.emit(false); // any thing passed,which want to emit,bolean/object too
    return console.log('cancelled');
  }
 
}
