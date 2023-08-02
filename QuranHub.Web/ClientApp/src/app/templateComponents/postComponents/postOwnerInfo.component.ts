import { Component, Input } from '@angular/core';
import { Post } from 'src/app/models/post/post.model';

@Component({
  selector: "postOwnerInfo",
  templateUrl: "postOwnerInfo.component.html"
})

export class PostOwnerInfoComponent {

  @Input()
  user!: any;

  @Input()
  post!: Post;
}
