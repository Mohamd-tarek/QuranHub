import { Component, Input } from '@angular/core';

@Component({
  selector: "commentCount",
  templateUrl: "commentCount.component.html"
})

export class CommentCountComponent {

  @Input()
  count!: number;
 
}
