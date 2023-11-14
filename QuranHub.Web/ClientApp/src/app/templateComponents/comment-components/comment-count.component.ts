import { Component, Input } from '@angular/core';

@Component({
  selector: "comment-count",
  templateUrl: "comment-count.component.html"
})

export class CommentCountComponent {

  @Input()
  count!: number;
 
}
