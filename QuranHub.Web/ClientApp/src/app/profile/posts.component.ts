import { Component, Input } from '@angular/core';
import { Post } from '../models/post/post.model';

@Component({
  selector: "posts",
  templateUrl: "posts.component.html"
})

export class PostsComponent {

 @Input()
 posts!:Post[]; 
 
 }
