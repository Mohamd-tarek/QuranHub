import { Component, Input, Output, EventEmitter } from '@angular/core';
@Component({
  selector: "like-count",
  templateUrl: "like-count.component.html"
})

export class LikeCountComponent {

  @Input()
  count!: number;
  
  @Input()
  addYou!: boolean;

  @Output()
  showLikesEvent = new EventEmitter();

  showLikes() {
    this.showLikesEvent.emit();
  }
}
