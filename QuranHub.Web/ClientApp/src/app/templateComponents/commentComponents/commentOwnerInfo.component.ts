import { Component, Input } from '@angular/core';
import { Comment } from 'src/app/models/post/comment.model';

@Component({
  selector: "commentOwnerInfo",
  templateUrl: "commentOwnerInfo.component.html"
})

export class CommentOwnerInfo {

  @Input()
  user!: any;

  @Input()
  comment!: Comment;
}
