import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../_service/auth.service';
import { AlertifyService } from '../_service/alertify.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { BsDatepickerConfig } from 'ngx-bootstrap';
import { User } from '../_models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  // model: any = {};
  user: User;
  registerForm: FormGroup;
  bsConfig: Partial<BsDatepickerConfig>;
  @Input() valuesFromHome: any;
  @Output() cancelRegister = new EventEmitter();


  constructor(private http: HttpClient, private authService: AuthService,
              private alertify: AlertifyService, private fb: FormBuilder,
              private router: Router ) { }
  // FormBuilder is used for reactive form
  ngOnInit() {
    this.createRegisterForm();
    this.bsConfig = {
      containerClass : 'theme-red', // theme-red,theme-green, theme-dark-blue
      // There are 6 color schemes: theme-default, theme-green, theme-blue, theme-dark-blue, theme-red, theme-orange
    };
    /* this.registerForm = new FormGroup({
       username : new FormControl('', Validators.required),
       password : new FormControl('', [Validators.required,
                           Validators.minLength(4), Validators.maxLength(8)]
                             ),
       confirmPassword : new FormControl('', Validators.required),
     }, this.passwordMatchValidator);
     */
  }

  createRegisterForm() {
    this.registerForm = this.fb.group({
      gender : ['male'],
      username: ['', Validators.required],
      nickName : ['', Validators.required],
      dateOfBirth : [null, Validators.required],
      city : ['', Validators.required],
      country : ['', Validators.required],
      password: ['', [Validators.required,
      Validators.minLength(4), Validators.maxLength(8)]
      ],
      confirmPassword: ['', Validators.required]
    }, { validator: this.passwordMatchValidator });
  }
  passwordMatchValidator(g: FormGroup) {
    return g.get('password').value === g.get('confirmPassword').value ? null : { 'mismatch': true };
  }

  register() {
if (this.registerForm.valid) {
      this.user = Object.assign({}, this.registerForm.value);
      this.authService.register(this.user).subscribe(next => {
          this.alertify.success('Registeration Succesfully');
      }, error => {
        this.alertify.error(error);
      }, () => {
            this.authService.login(this.user).subscribe(next => {
              this.router.navigate(['/members']);
            });
      });
}
      // console.log(this.registerForm.value);
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
