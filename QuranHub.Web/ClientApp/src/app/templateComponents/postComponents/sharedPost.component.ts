import { Component, Input, OnInit, ViewContainerRef } from '@angular/core';
import { Post } from 'src/app/models/post/post.model';
import { PostRepository } from '../../abstractions/repositories/postRepository';
import { UserService } from '../../abstractions/services/userService';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";
import { FadeOutTrigger } from 'src/app/animations/FadeOut.animation';


@Component({
  selector: "sharedPost",
  templateUrl: "sharedPost.component.html",
  animations: [FadeOutTrigger]

})

export class SharedPostComponent implements OnInit{

  @Input()
  post!: Post;

  user: UserBasicInfo;
  writingComment: boolean = false;
  commentAdded: boolean = false;
  viewAya: boolean = true;
  showLikes: boolean = false;
  isOwner: boolean = false;
  show: boolean = true;
  editPost: boolean = false;
  
  constructor(
    public postDataRepository: PostRepository,
    public userService: UserService,
    private ref: ViewContainerRef) {
    this.user = userService.getUser() as UserBasicInfo;
  }

  ngOnInit(): void {
    this.isOwner = this.user.id == this.post.quranHubUser.id;
    if (this.post.verse.aya == this.post.share.post.verse.aya &&
      this.post.verse.sura == this.post.share.post.verse.sura ){
      this.viewAya = false;
    }
  }

  showLikesEvent() {
    this.showLikes = true;
  }

  hideLikesEvent() {
    this.showLikes = false;
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
