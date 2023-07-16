import { Component } from '@angular/core';
import { Post } from 'src/app/models/post/post.model';
import { HomeService } from '../abstractions/services/homeService';
import { AuthenticationService } from "../abstractions/services/authenticationService"

@Component({
  selector: "home",
  templateUrl: "home.component.html"
})

export class HomeComponent {

  posts : Post[] = [];
  dataLoaded : boolean = false;

  constructor(public homeDataService: HomeService,  public authService: AuthenticationService) {
    this.homeDataService.getNewFeeds().subscribe( posts => {
      console.log(posts);
      this.posts = posts;
      this.dataLoaded = true;
    })
  }

  postAddedEvent(post: Post){
      this.posts.unshift(post);
  }
 
}
