import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: "comment-text",
  templateUrl: "comment-text.component.html"
})

export class CommentText implements OnInit {

  @Input()
  text!: string;

  wrap:boolean = false;

  ngOnInit(){
    this.wrap = this.text.length > 250;
  }
}
