import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators'; // observavales
import {JwtHelperService} from '@auth0/angular-jwt';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

constructor(private http: HttpClient ) { }

  baseUrl = 'http://localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();
  decodedToken : any;

  login(model: any) {

    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const _response = response;
        if (_response) {
        localStorage.setItem('token', _response.token);
        this.decodedToken = this.jwtHelper.decodeToken(_response.token);
        console.log(this.decodedToken);
        }
      })
    );
  }

  register(model: any) {

    return this.http.post(this.baseUrl + 'register', model).pipe(
      map((response: any) => {
        const _response = response;
        if (_response) {
        localStorage.setItem('token', _response.token);
        }
      })
    );
  }


  loggedIn() {
  const token = localStorage.getItem('token');
  return !this.jwtHelper.isTokenExpired(token);

  }
}
