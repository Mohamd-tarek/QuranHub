import { Component, Input, OnInit, ViewContainerRef} from '@angular/core';
import { Post } from 'src/app/models/post/post.model';
import { PostRepository } from '../../abstractions/repositories/postRepository';
import { UserService } from '../../abstractions/services/userService';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";
import { FadeOutTrigger } from 'src/app/animations/FadeOut.animation';

@Component({
  selector: "post",
  templateUrl: "post.component.html",
  animations: [FadeOutTrigger]
})

export class PostComponent implements OnInit {

  @Input()
  post!: Post;

  user: UserBasicInfo;
  writingComment: boolean = false;
  commentAdded: boolean = false;
  shareStarted: boolean = false;
  showLikes: boolean = false;
  showShares: boolean = false;
  isOwner: boolean = false;
  show: boolean = true;
  editPost: boolean = false;
  
  constructor(
    public postDataRepository: PostRepository,
    public userService: UserService,
    private vcRef : ViewContainerRef 
  ) {
    this.user = userService.getUser() as UserBasicInfo;
  }

  ngOnInit() {
    this.isOwner = this.user.id == this.post.quranHubUser.id;
  }


  likeEvent(){
    this.post.reactedTo = true;
    this.postDataRepository.addReact(1, this.post.postId).subscribe(like => {
       this.post.reactedTo = true;
       this.post.reactsCount++;
    },
    error => {
      this.post.reactedTo = false;
    });

  }

  unlikeEvent(){
    this.post.reactedTo = false;
    this.postDataRepository.removeReact(this.post.postId).subscribe(response => {
        this.post.reactedTo = false;
        this.post.reactsCount--; 
    },
    error => {
      this.post.reactedTo = true;
    });

  }

  showLikesEvent() {
    this.showLikes = true;
  }

  hideLikesEvent() {
    this.showLikes = false;
  }

  showSharesEvent() {
    this.showShares = true;
  }

  hideSharesEvent() {
    this.showShares = false;
  }

  shareStartEvent(){
    this.shareStarted = true;
  }

  shareDoneEvent(){
    this.shareStarted = false;
  }

  startWritingComment(){
    this.writingComment = true;
  }

  cancelWritingComment(){
    this.writingComment = false;
  }

  editEvent() {
    this.editPost = true;
  }

  editedEvent() {
    this.editPost = false;
  }

  deleteEvent() {
    this.show = false;
   
   // this.vcRef.clear();   not working

  }

}
