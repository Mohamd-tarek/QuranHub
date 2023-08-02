import { Component, Output, EventEmitter } from '@angular/core';

@Component({
  selector: "addComment",
  templateUrl: "addComment.component.html"
})

export class AddCommentComponent {
   
  @Output()
  writingCommentEvent = new EventEmitter();

  onWritingComment(){
    this.writingCommentEvent.emit();
  }
}
