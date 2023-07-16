import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Post } from 'src/app/models/post/post.model';
import { HomeService } from '../abstractions/services/homeService';

@Component({
  selector: 'postsResults',
  templateUrl: './postsResults.component.html',
})

export class PostsResultsComponent  {

  query!: string;
  posts: Post[] = [];
  dataLoaded: boolean = false;

  constructor(
    private activeRoute: ActivatedRoute,
    private homeDataService: HomeService) {

    this.activeRoute.params.subscribe( params => {
      this.query = params["q"];
      this.homeDataService.searchPosts(this.query).subscribe(result => {
        this.posts = result;
        this.dataLoaded = true;
      });
      });
  }
 
}
