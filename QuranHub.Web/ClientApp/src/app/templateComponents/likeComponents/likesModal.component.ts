import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";
import { React } from "../../models/post/react.model";
import { PostRepository } from '../../abstractions/repositories/postRepository';

@Component({
  selector: "likesModal",
  templateUrl: "likesModal.component.html"
})

export class LikesModalComponent  implements OnInit{
 
  @Input()
  postId!: any;

  @Input()
  repository!: any;

  @Output()
  hideLikesEvent = new EventEmitter();



  users: UserBasicInfo[] = [];
  postReacts: React[] = [];

  ngOnInit(): void {
    this.repository.loadMoreReacts(this.postId, this.users.length, 50).subscribe((postReacts: React[]) => {
      this.postReacts = postReacts;
      console.log(postReacts);
      postReacts.forEach(postReact => this.users.push(postReact.quranHubUser));
    })
  }

  hideLikes() {
    this.hideLikesEvent.emit();
  }

}
