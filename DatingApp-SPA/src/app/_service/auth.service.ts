import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators'; // observavales


@Injectable({
  providedIn: 'root'
})
export class AuthService {

constructor(private http: HttpClient ) { }

  baseUrl= 'http://localhost:5000/api/auth/';

  login(model: any){

    return this.http.post(this.baseUrl + 'login', model).pipe(
      map((response: any) => {
        const _response = response;
        if(_response){
        localStorage.setItem('token', _response.token);
        }
      })
    );
}

}
