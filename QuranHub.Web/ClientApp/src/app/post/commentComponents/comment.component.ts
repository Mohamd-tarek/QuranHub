import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Comment } from 'src/app/models/post/comment.model';
import { UserService } from '../../abstractions/services/userService';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";
import { PostRepository } from '../../abstractions/repositories/postRepository';

@Component({
  selector: "comment",
  templateUrl: "comment.component.html"
})

export class CommentComponent implements OnInit {
  
  @Input()
  comment!: Comment;

  isOwner:boolean = false;
  submitDelete:boolean = false;
  showLikes: boolean = false;

  user: UserBasicInfo;

  @Output()
  commentRemoveEvent = new EventEmitter<number>();

  constructor(
    public userService: UserService,
    public postDataRepository: PostRepository) {
    this.user = userService.getUser() as UserBasicInfo;
  }

  ngOnInit(): void {
    this.isOwner = this.user.id == this.comment.quranHubUser.id;
  }

  deleteComment(){
     this.submitDelete = true;
    this.postDataRepository.removeComment(this.comment.commentId).subscribe((response: any) => {
        this.commentRemoveEvent.emit(this.comment.commentId);
        this.submitDelete = false;
        
      })
  }

  likeEvent(){
    this.comment.reactedTo = true;
    this.postDataRepository.addCommentReact(1, this.comment.commentId, this.user.id).subscribe((response:any) => {
       this.comment.reactedTo = true;
       this.comment.reactsCount++;
      
    },
    () => {
      this.comment.reactedTo = false;
    });

  }

  unlikeEvent(){
    this.comment.reactedTo = false;
    this.postDataRepository.removeCommentReact(this.comment.commentId).subscribe((response: any) => {
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
