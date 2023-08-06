import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";
import { React } from "../../models/post/react.model";
import { PostRepository } from '../../abstractions/repositories/postRepository';
import { Share } from '../../models/post/share.model';

@Component({
  selector: "sharesModal",
  templateUrl: "sharesModal.component.html"
})

export class SharesModalComponent  implements OnInit{
 
  @Input()
  postId!: any;

  @Input()
  repository!: any;

  @Input()
  totalShares!: number;

  @Output()
  hideSharesEvent = new EventEmitter();

  loading: boolean = false;

  users: UserBasicInfo[] = [];

  postShare: Share[] = [];

  ngOnInit(): void {
    this.loadMoreShares();
  }

  hideShares() {
    this.hideSharesEvent.emit();
  }

  loadMoreShares() {
    this.loading = true;
    this.repository.loadMoreShares(this.postId, this.users.length, 50).subscribe((postShares: Share[]) => {
      this.postShare = postShares;
      console.log(postShares);
      postShares.forEach(postShare => this.users.push(postShare.quranHubUser));
      this.loading = false;
    })
  }

}
