import { Component, Input } from '@angular/core';
import { Post } from '../models/post/post.model';
import { ProfileService } from '../abstractions/services/profileService';
import { UserBasicInfo } from "../models/user/userBasicInfo.model";
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: "profileDetails",
  templateUrl: "profileDetails.component.html"
})

export class ProfileDetailsComponent {

  posts!:Post[]; 
  followings!: UserBasicInfo[];
  followers!: UserBasicInfo[];
  userId!:string;
 
  constructor(
   public profileServeice: ProfileService,
    private activeRoute: ActivatedRoute) {
    console.log(this.activeRoute.parent?.snapshot);
    this.userId = this.activeRoute.parent?.snapshot.params["userId"];
 
  }

  ngOnInit(): void {
    this.profileServeice.getUserPosts(this.userId).subscribe(posts => {
      this.posts = posts;
      console.log(this.posts);
    });

    this.profileServeice.getUserFollowings(this.userId).subscribe(followings => {
      this.followings = followings;
    });

    this.profileServeice.getUserFollowers(this.userId).subscribe(followers => {
      this.followers = followers;
    })
  }
}
