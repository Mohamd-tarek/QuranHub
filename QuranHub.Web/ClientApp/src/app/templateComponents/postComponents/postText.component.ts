import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: "postText",
  templateUrl: "postText.component.html"
})

export class PostTextComponent implements OnInit {

  @Input()
  text!: string;

  wrap:boolean = false;

  ngOnInit(){
    this.wrap = this.text.length > 250;
  }
}
