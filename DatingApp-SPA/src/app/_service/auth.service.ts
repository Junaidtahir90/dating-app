import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject}  from 'rxjs';
import {map} from 'rxjs/operators'; // observavales
import {JwtHelperService} from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

constructor(private http: HttpClient ) { }

  baseUrl = environment.apiUrl + 'auth/';
  jwtHelper = new JwtHelperService();
  decodedToken: any;
  currentUser: User;
  photoUrl = new BehaviorSubject<string>('../../assets/user.png')
  currentPhotoUrl = this.photoUrl.asObservable();

  changeMemberPhoto (photoUrl : string){
    this.photoUrl.next(photoUrl);
  }

  login(model: any) {

    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        // tslint:disable-next-line: variable-name
        const _response = response;
        if (_response) {
        localStorage.setItem('token', _response.token);
        localStorage.setItem('user', JSON.stringify(_response.user));
        this.currentUser = _response.user;
        this.decodedToken = this.jwtHelper.decodeToken(_response.token);
        this.changeMemberPhoto(this.currentUser.photoUrl);
        console.log(this.decodedToken);
        }
      })
    );
  }

  register(model: any) {

    return this.http.post(this.baseUrl + 'register', model).pipe(
      map((response: any) => {
        const Response = response;
        if (Response) {
        localStorage.setItem('token', Response.token);
        }
      })
    );
  }


  loggedIn() {
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);

  }
}
