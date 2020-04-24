import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  registerMode = false;
  values: any;
  // http://localhost:5000/api/values/
  baseUrl = environment.apiUrl; // 'http://localhost:5000/api/';

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues();
  }


  registerToggle() {
    this.registerMode = true; // !this.registerMode
  }
  getValues() {
    this.http.get(this.baseUrl + 'values/').subscribe(response => {
      this.values = response;
    }, error => {
      console.log(error);
    });
  }

  cancelRegisterMode(registerMode: boolean) {

    this.registerMode = registerMode; // !this.registe
  }
}
