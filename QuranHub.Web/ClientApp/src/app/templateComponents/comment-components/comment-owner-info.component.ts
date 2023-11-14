import { Component, Input } from '@angular/core';
import { Comment } from 'src/app/models/post/comment.model';

@Component({
  selector: "comment-owner-info",
  templateUrl: "comment-owner-info.component.html"
})

export class CommentOwnerInfo {

  @Input()
  user!: any;

  @Input()
  comment!: Comment;
}
