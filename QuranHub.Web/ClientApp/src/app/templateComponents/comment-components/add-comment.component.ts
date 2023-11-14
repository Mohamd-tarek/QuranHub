import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: "add-comment",
  templateUrl: "add-comment.component.html"
})

export class AddCommentComponent {
   
  @Output()
  writingCommentEvent = new EventEmitter();

  onWritingComment(){
    this.writingCommentEvent.emit();
  }
}
