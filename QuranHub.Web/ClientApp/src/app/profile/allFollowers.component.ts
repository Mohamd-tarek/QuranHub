import { Component, Input } from '@angular/core';
import { ProfileService } from '../abstractions/services/profileService';
import { ActivatedRoute, Router } from '@angular/router';
import { UserBasicInfo } from "../models/user/userBasicInfo.model";

@Component({
  selector: "allFollowers",
  templateUrl: "allFollowers.component.html"
})

export class AllFollowersComponent {

  userId!:string;
  users!: UserBasicInfo[];
  dataLoaded:boolean = false;
  
  constructor(
    public profileService: ProfileService,
    private activeRoute: ActivatedRoute) {
    this.userId = this.activeRoute.parent?.snapshot.params["userId"];

    this.profileService.getUserFollowers(this.userId).subscribe( users => {
      this.users= users;
      this.dataLoaded = true;
    }); 
  }

  getFollowers(value: string){
    this.profileService.getUserFollowersByKeyWord(this.userId, value).subscribe( users => {
      this.users= users;
      this.dataLoaded = true;
       });
  }

}

