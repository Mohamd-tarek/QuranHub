import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommentRepository } from '../../abstractions/repositories/CommentRepository';

@Component({
  selector: "viewMoreComments",
  templateUrl: "viewMoreComments.component.html"
})

export class ViewMoreCommentsComponent {
  
  @Input()
  post!: any;

  @Input()
  repository!: CommentRepository;

  @Input()
  totalComments!: number;

  @Input()
  loadedComments!: number;

  firstLoad:boolean = false;

  loading:boolean = false;

  @Output() moreCommentLoadedEvent = new EventEmitter();


  onLoadMoreComment(){
    let id: number = this.findValueBySuffix(this.post, "Id");

    if(this.firstLoad == false){
      this.moreCommentLoadedEvent.emit();
      this.firstLoad = true;
    } else {
      this.loading = true;
      this.repository.loadMoreComments(id, this.loadedComments, 50).subscribe( (comments:any) => {
        this.post.comments.push(...comments);
        this.loadedComments = this.post.comments.length;
        this.moreCommentLoadedEvent.emit();
        this.loading = false;
      })
    }
  }

  findValueBySuffix(object: any, key: any) {
    for (var property in object) {
      if (object.hasOwnProperty(property) &&
        property.toString().endsWith(key)) {
        return object[property];
      }
    }
  }
}
