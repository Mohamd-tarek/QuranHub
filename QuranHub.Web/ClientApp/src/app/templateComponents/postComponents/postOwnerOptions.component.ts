import { Component, Input, Output, EventEmitter } from '@angular/core';
import { PostRepository } from '../../abstractions/repositories/postRepository';
import { Post } from 'src/app/models/post/post.model';

@Component({
  selector: "postOwnerOptions",
  templateUrl: "postOwnerOptions.component.html"
})

export class PostOwnerOptionsComponent {

  constructor(
    public postDataRepository: PostRepository) {
  }

  @Input()
  post!: Post;

  @Output()
  deleteEvent = new EventEmitter();

  @Output()
  editEvent = new EventEmitter();

  loading: boolean = false;

  delete() {
    this.loading = true;
    this.postDataRepository.deletePost(this.post.postId).subscribe(result => {
      this.loading = false;
      this.deleteEvent.emit();
    }, () => {
      this.loading = false;
    })
  }

  edit() {
    this.editEvent.emit();
  }


  
}
