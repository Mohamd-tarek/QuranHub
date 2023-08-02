import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Comment } from 'src/app/models/post/comment.model';
import { CommentRepository } from '../../abstractions/repositories/CommentRepository';
import { UserService } from '../../abstractions/services/userService';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";

@Component({
  selector: "commentContainer",
  templateUrl: "commentContainer.component.html"
})

export class CommentContainerComponent implements OnInit {
  
  user: UserBasicInfo;

  @Input()
  post!: any;

  @Input()
  repository!: CommentRepository;

  @Input()
  writingComment!: boolean;

  @Output()
  cancelWritingCommentEvent = new EventEmitter();

  verseId!:number | null

  commentAdded: boolean = false;
  viewedComments: Comment[] = [];

  constructor(
    public userService: UserService) {
    this.user = userService.getUser() as UserBasicInfo;
  }

  ngOnInit(): void {
    if (this.post.comments.length) {
      this.viewedComments.push(this.post.comments[0]);
    }
  }

  updateViewedComments(){
    this.viewedComments = this.post.comments;
  }

  cancelWritingComment(){
    this.writingComment = false;
    this.cancelWritingCommentEvent.emit();
  }

  choosingAyaEvent(verseId: any){
    this.verseId = verseId;
  }

  addingCommentEvent(comment: any) {
    let id: number = this.findValueBySuffix(this.post, "Id");

    this.repository.addComment(comment, this.user.id, id, this.verseId).subscribe((comment:any) => {
      this.commentAdded = comment !== null;
      this.writingComment = false;
      this.cancelWritingCommentEvent.emit();
      this.post.comments.push(comment);
      this.post.commentsCount++;
      this.verseId = null;
      this.updateViewedComments();
      console.log(this.post);
      console.log(this.viewedComments);
    })
  }

  commentRemoveEvent(commentId: any){
     let commentIndex = this.post.comments.findIndex((comment:any) => comment.commentId == commentId);
     this.post.comments.splice(commentIndex, 1);
     this.post.commentsCount--;
     this.updateViewedComments();
  }

  findValueBySuffix(object:any, key:any) {
  for (var property in object) {
    if (object.hasOwnProperty(property) &&
      property.toString().endsWith(key)) {
      return object[property];
    }
  }
}
}

