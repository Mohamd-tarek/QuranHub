import { Component, Output, Input, EventEmitter } from '@angular/core';
@Component({
  selector: "like",
  templateUrl: "like.component.html"
})

export class LikeComponent {

  @Output()
  likeEvent = new EventEmitter();

  @Output()
  unlikeEvent = new EventEmitter();

  @Input()
  liked: boolean = false;

  onLike(){
    this.likeEvent.emit();
  }

  onUnlike(){
    this.unlikeEvent.emit();
  }
 
}
