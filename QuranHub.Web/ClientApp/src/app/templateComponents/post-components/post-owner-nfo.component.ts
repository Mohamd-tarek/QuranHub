import { Component, Input } from '@angular/core';
import { Post } from 'src/app/models/post/post.model';

@Component({
  selector: "post-owner-nfo",
  templateUrl: "post-owner-nfo.component.html"
})

export class PostOwnerInfoComponent {

  @Input()
  user!: any;

  @Input()
  post!: Post;
}
