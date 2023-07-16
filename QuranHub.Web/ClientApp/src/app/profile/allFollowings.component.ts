import { Component, Input } from '@angular/core';
import { ProfileService } from '../abstractions/services/profileService';
import { ActivatedRoute, Router } from '@angular/router';
import { UserBasicInfo } from "../models/user/userBasicInfo.model";

@Component({
  selector: "allFollowings",
  templateUrl: "allFollowings.component.html"
})

export class AllFollowingsComponent {

  userId!:string;
  users!: UserBasicInfo[];
  dataLoaded: boolean = false;

  constructor(
    public profileService: ProfileService,
    private activeRoute: ActivatedRoute) {
    this.userId = this.activeRoute.parent?.snapshot.params["userId"];
    this.profileService.getUserFollowings(this.userId).subscribe( users => {
      this.users= users;
      this.dataLoaded = true;
    });
  }

  getFollowings(value: string){
    this.profileService.getUserFollowingsByKeyWord(this.userId, value).subscribe( users => {
      this.users= users;
      this.dataLoaded = true;
       });
  }

}
