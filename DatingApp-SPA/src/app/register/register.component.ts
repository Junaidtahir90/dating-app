import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../_service/auth.service';
import { AlertifyService } from '../_service/alertify.service';
import { FormGroup, FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  model: any = {};
  registerForm: FormGroup;
  @Input() valuesFromHome: any;
  @Output () cancelRegister = new EventEmitter();


  constructor(private http: HttpClient, private authService: AuthService, private alertify: AlertifyService ) { }

  ngOnInit() {
    this.registerForm = new FormGroup({
      username : new FormControl('username',Validators.required),
      password : new FormControl('', [Validators.required,
                          Validators.minLength(4), Validators.maxLength(8)]
                            ),
      confirmPassword : new FormControl('', Validators.required),
    },this.passwordMatchValidator);
  }
  passwordMatchValidator ( g : FormGroup) {
return g.get('password').value === g.get('confirmPassword').value ? null : { 'mismatch' : true };
  }

  register() {

    console.log(this.registerForm.value);
    /*this.authService.register(this.model).subscribe(next => {
      this.alertify.success('Register Succesfully');
    }, error => {
      this.alertify.error(error);
    });*/
   // return console.log(this.model);
  }

  cancel() {
    this.cancelRegister.emit(false); // any thing passed,which want to emit,bolean/object too
  }

}
