import { Component, Output, EventEmitter, Input } from '@angular/core';
import { Post } from 'src/app/models/post/post.model';

@Component({
  selector: "writingComment",
  templateUrl: "writingComment.component.html"
})

export class WritingCommentComponent {

  @Input()
  chosenSura: number = 1;

  @Input()
  chosenAya: number = 1;

  verseId! :number;
  comment: string = "";
  submitted:boolean = false;
  addAya:boolean = false;

 
  @Output()
  cancelWritingCommentEvent = new EventEmitter();

  @Output()
  addingCommentEvent = new EventEmitter<string>();

  @Output()
  choosingAyaEvent = new EventEmitter<number>();

  onCancelWritingComment(){
    this.cancelWritingCommentEvent.emit();
  }

  onChooseAyaEvent(verse: any){
    this.choosingAyaEvent.emit(verse.verseId);
  }

  onAddingComment(){
    this.submitted = true;
    this.addingCommentEvent.emit(this.comment);
  }

  addingAya(){
    this.addAya = true;

    
  }
  

}
