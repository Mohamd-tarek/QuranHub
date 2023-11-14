import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: "post-text",
  templateUrl: "post-text.component.html"
})

export class PostTextComponent implements OnInit {

  @Input()
  text!: string;

  wrap:boolean = false;

  ngOnInit(){
    this.wrap = this.text.length > 250;
  }
}
