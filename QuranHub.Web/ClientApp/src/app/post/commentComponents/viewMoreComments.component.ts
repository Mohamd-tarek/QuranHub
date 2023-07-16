import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Post } from '../../models/post/post.model';
import { PostRepository } from '../../abstractions/repositories/postRepository';

@Component({
  selector: "viewMoreComments",
  templateUrl: "viewMoreComments.component.html"
})

export class ViewMoreCommentsComponent {
  
  @Input()
  post!: Post;

  @Input()
  totalComments!: number;

  @Input()
  loadedComments!: number;

  firstLoad:boolean = false;

  loading:boolean = false;

  @Output() moreCommentLoadedEvent = new EventEmitter();

  constructor( public postDataRepository: PostRepository) {}

  onLoadMoreComment(){
    
    if(this.firstLoad == false){
      this.moreCommentLoadedEvent.emit();
      this.firstLoad = true;
    } else {
      this.loading = true;
      this.postDataRepository.loadMoreComments(this.post.postId, this.loadedComments, 50).subscribe( (comments:any) => {
        this.post.comments.push(...comments);
        this.loadedComments = this.post.comments.length;
        this.moreCommentLoadedEvent.emit();
        this.loading = false;
      })
    }
  }
}
