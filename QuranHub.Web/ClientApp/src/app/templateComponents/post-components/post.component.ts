import { Component, Input, OnInit, ViewContainerRef, inject} from '@angular/core';
import { Post } from 'src/app/models/post/post.model';
import { PostRepository } from '../../abstractions/repositories/postRepository';
import { UserService } from '../../abstractions/services/userService';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";
import { FadeOutTrigger } from 'src/app/animations/FadeOut.animation';
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ShareModalComponent } from '../share-components/share-modal.component';
import { SharesModalComponent } from '../share-components/shares-modal.component';
import { LikesModalComponent } from '../like-components/likes-modal.component';


@Component({
  selector: "post",
  templateUrl: "post.component.html",
  animations: [FadeOutTrigger]
})

export class PostComponent implements OnInit {

  private modalService = inject(NgbModal);

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
    const modalRef = this.modalService.open(LikesModalComponent);
    modalRef.componentInstance.repository = this.postDataRepository;
    modalRef.componentInstance.postId = this.post.postId;
    modalRef.componentInstance.totalLikes = this.post.reactsCount;
    this.showLikes = true;
    
  }

  hideLikesEvent() {
    
    this.showLikes = false;
  }

  showSharesEvent() {
    const modalRef = this.modalService.open(SharesModalComponent);
    modalRef.componentInstance.repository = this.postDataRepository;
    modalRef.componentInstance.postId = this.post.postId;
    modalRef.componentInstance.totalShares = this.post.sharesCount;
    this.showShares = true;
  }

  hideSharesEvent() {
    this.showShares = false;
  }

  shareStartEvent(){
    const modalRef = this.modalService.open(ShareModalComponent);
    modalRef.componentInstance.repository = this.postDataRepository;
    modalRef.componentInstance.post = this.post;
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
