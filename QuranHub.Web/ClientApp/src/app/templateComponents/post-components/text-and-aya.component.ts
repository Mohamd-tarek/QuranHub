import { Component, Input } from '@angular/core';
import { Post } from 'src/app/models/post/post.model';

@Component({
  selector: "text-and-aya",
  templateUrl: "text-and-aya.component.html"
})

export class TextAndAyaComponent  {
  
  @Input()
  post!: Post;

  @Input()
  viewAya: boolean = true;
}

