import { Component, Input } from '@angular/core';
import { ProfileService } from 'src/app/abstractions/services/profileService';
import { UserService } from '../abstractions/services/userService';
import { UserBasicInfo } from "../models/user/userBasicInfo.model";
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: "cover",
  templateUrl: "cover.component.html"
})

export class CoverComponent  { 

  user!: UserBasicInfo; 
  currentUser!: UserBasicInfo;
  @Input()
  picture: any;
  userId!:string;
  

  constructor(
    public profileService: ProfileService,
    public userService: UserService,
    private activeRoute: ActivatedRoute) {
    this.currentUser = this.userService.getUser() as UserBasicInfo;
    this.userId = this.activeRoute.snapshot.params["userId"];
  }

  onFileSelected(formData :any) {

      this.profileService.editCoverPicture(formData).subscribe(result =>{
         if(result != null){
          this.picture = result;
          }
        });
    }
}
