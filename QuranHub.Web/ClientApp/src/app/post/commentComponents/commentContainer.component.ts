import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Comment } from 'src/app/models/post/comment.model';
import { Post } from 'src/app/models/post/post.model';
import { UserService } from '../../abstractions/services/userService';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";
import { PostRepository } from '../../abstractions/repositories/postRepository';

@Component({
  selector: "commentContainer",
  templateUrl: "commentContainer.component.html"
})

export class CommentContainerComponent implements OnInit {
  
  user: UserBasicInfo;

  @Input()
  post!: Post;

  @Input()
  writingComment!: boolean;

  @Output()
  cancelWritingCommentEvent = new EventEmitter();

  verseId!:number | null

  commentAdded: boolean = false;
  viewedComments: Comment[] = [];

  constructor(
    public userService: UserService,
    public postDataRepository: PostRepository) {
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

  addingCommentEvent(comment: any){
    this.postDataRepository.addComment(comment, this.user.id, this.post.postId, this.verseId).subscribe((comment:any) => {
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
     let commentIndex = this.post.comments.findIndex(comment => comment.commentId == commentId);
     this.post.comments.splice(commentIndex, 1);
     this.post.commentsCount--;
     this.updateViewedComments();
  }

}

