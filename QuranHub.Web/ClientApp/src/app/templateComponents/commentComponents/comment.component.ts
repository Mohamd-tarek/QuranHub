import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Comment } from 'src/app/models/post/comment.model';
import { CommentRepository } from '../../abstractions/repositories/CommentRepository';
import { UserService } from '../../abstractions/services/userService';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";

@Component({
  selector: "comment",
  templateUrl: "comment.component.html"
})

export class CommentComponent implements OnInit {
  
  @Input()
  comment!: Comment;

  @Input()
  repository!: CommentRepository;

  isOwner: boolean = false;

  submitDelete: boolean = false;

  showLikes: boolean = false;

  user: UserBasicInfo;

  @Output()
  commentRemoveEvent = new EventEmitter<number>();

  constructor(
    public userService: UserService) {
    this.user = userService.getUser() as UserBasicInfo;
  }

  ngOnInit(): void {
    this.isOwner = this.user.id == this.comment.quranHubUser.id;
  }

  deleteComment(){
     this.submitDelete = true;
    this.repository.removeComment(this.comment.commentId).subscribe((response: any) => {
        this.commentRemoveEvent.emit(this.comment.commentId);
        this.submitDelete = false;
        
      })
  }

  likeEvent(){
    this.comment.reactedTo = true;
    this.repository.addCommentReact(1, this.comment.commentId, this.user.id).subscribe((response:any) => {
       this.comment.reactedTo = true;
       this.comment.reactsCount++;
      
    },
    () => {
      this.comment.reactedTo = false;
    });

  }

  unlikeEvent(){
    this.comment.reactedTo = false;
    this.repository.removeCommentReact(this.comment.commentId).subscribe((response: any) => {
        this.comment.reactedTo = false;
        this.comment.reactsCount--; 
    },
    () => {
      this.comment.reactedTo = true;
    });

  }

  showLikesEvent() {
    this.showLikes = true;
  }

  hideLikesEvent() {
    this.showLikes = false;
  }
 
}
