import { Component, Output, EventEmitter, Input } from '@angular/core';
import { PostRepository } from '../../abstractions/repositories/postRepository';
import { Post } from 'src/app/models/post/post.model';
import { Privacy } from 'src/app/models/post/post.model';
import { Quran } from "src/app/models/quran/quran.model";

@Component({
  selector: "editPost",
  templateUrl: "editPost.component.html"
})

export class EditPostComponent {

  submitted:boolean = false;

  @Input()
  post!: Post;

  @Output()
  postEditedEvent = new EventEmitter();

  verses: Quran[] = [];

  constructor(
    public postDataRepository: PostRepository) {
        
   }
  
  chooseAyaEvent(verse: any) {
    this.post.verse = verse;

  }

  choosePrivacyEvent(privacy: Privacy){
    this.post.privacy = privacy;
  }

  CancelEditingPost() {
    this.postEditedEvent.emit();
  }

  savePost(){
    this.submitted = true;
    this.postDataRepository.editPost(this.post.postId, this.post.verse.index, this.post.text, this.post.privacy).subscribe(response => {
      console.log(response);
          this.submitted = false;
          this.postEditedEvent.emit();
         
     })
  }
}
