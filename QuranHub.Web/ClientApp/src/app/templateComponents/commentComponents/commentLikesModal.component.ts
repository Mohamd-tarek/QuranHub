import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";

import { React } from "../../models/post/react.model";

@Component({
  selector: "commentLikesModal",
  templateUrl: "commentLikesModal.component.html"
})

export class CommentLikesModalComponent implements OnInit {
 
  ngOnInit(): void {
    this.loadMoreLikes();
  }

  @Input()
  commentId!: any;

  @Input()
  repository!: any;

  @Input()
  totalLikes!: number;

  @Output()
  hideLikesEvent = new EventEmitter();

  commentReacts: React[] = [];

  loading: boolean = false;

  users: UserBasicInfo[] = [];


  hideLikes() {
    this.hideLikesEvent.emit();
  }
  loadMoreLikes() {
    this.loading = true;
    this.repository.loadMoreCommentReacts(this.commentId, this.users.length, 50).subscribe((commentReacts: React[]) => {
      this.commentReacts = commentReacts;
      console.log(commentReacts);
      commentReacts.forEach(commentReact => this.users.push(commentReact.quranHubUser));
      this.loading = false;
    })
  }

}
