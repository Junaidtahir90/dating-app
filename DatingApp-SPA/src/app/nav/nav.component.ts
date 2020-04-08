import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {


  model = {}; // not use any but {} object 
  // techSpecMeta: {};
  // Type script this means to declare a property of type {} with no value initialized. It is the same as:
  // techSpecMeta: Object
  constructor() { }

  ngOnInit() {
  }

  login(){
    console.log(this.model);
  }

}
