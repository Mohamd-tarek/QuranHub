import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";
import { PostReact } from "../../models/post/postReact.model";
import { PostRepository } from '../../abstractions/repositories/postRepository';

@Component({
  selector: "likesModal",
  templateUrl: "likesModal.component.html"
})

export class LikesModalComponent  implements OnInit{
 
  @Input()
  postId!: any;

  @Output()
  hideLikesEvent = new EventEmitter();

  users: UserBasicInfo[] = [];
  postReacts: PostReact[] = [];

  constructor(
    public postDataRepository: PostRepository) {
  }

  ngOnInit(): void {
    this.postDataRepository.loadMorePostReacts(this.postId, this.users.length, 50).subscribe((postReacts: PostReact[]) => {
      this.postReacts = postReacts;
      console.log(postReacts);
      postReacts.forEach(postReact => this.users.push(postReact.quranHubUser));
    })
  }

  hideLikes() {
    this.hideLikesEvent.emit();
  }

}
