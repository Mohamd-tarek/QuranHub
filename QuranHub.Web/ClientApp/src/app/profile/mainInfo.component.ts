import { Component, Input, OnInit } from '@angular/core';
import { ProfileService } from 'src/app/abstractions/services/profileService';
import { UserService } from '../abstractions/services/userService';
import { UserBasicInfo } from "../models/user/userBasicInfo.model";
import { Profile } from "../models/user/profile.model";
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: "mainInfo",
  templateUrl: "mainInfo.component.html"
})

export class MainInfoComponent { 

  user!: Profile; 
  currentUser!: UserBasicInfo;  
  userId:string;
  userLoaded: boolean = false;
  done: boolean = true;
  
  
  constructor(
    public profileService: ProfileService,
    public userService: UserService,
    private activeRoute: ActivatedRoute) {
    this.currentUser = this.userService.getUser() as UserBasicInfo;
    this.userId = this.activeRoute.snapshot.params["userId"];

    this.profileService.getUserProfile(this.userId).subscribe(result => {
        this.user = result;
        this.userLoaded = true;
    });

  }

  onFileSelected(formData: any) {
    this.done = false;
    this.profileService.editProfilePicture(formData).subscribe(result => {
      this.done = true;
      this.user.profilePicture = result;
      this.userService.updateUser(this.user);
    });
  }

}
