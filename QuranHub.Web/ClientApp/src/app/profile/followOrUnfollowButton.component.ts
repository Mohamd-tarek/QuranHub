import {Component, Input, OnInit } from "@angular/core";
import { UserService } from "../abstractions/services/userService";
import { UserBasicInfo } from "../models/user/userBasicInfo.model";
import { ProfileService } from "../abstractions/services/profileService";

@Component({
    selector: 'followOrUnfollowButton',
    templateUrl: "followOrUnfollowButton.component.html"
})

export class FollowOrUnfollowButtonComponent implements OnInit {

   @Input() 
   userId!: string;
   followed:boolean = false;
   loading:boolean = false;

  constructor(
    public profileService: ProfileService,
    public userService: UserService) { }

    ngOnInit(): void {
        this.checkFollowed();
    }

    checkFollowed(){
        this.profileService.checkFollowed(this.userId).subscribe(result => {
            this.followed = result;
         });

    }

    toggleFollow(){
        this.loading = true;
        if(!this.followed){
            let CurUser = this.userService.getUser() as UserBasicInfo;
            this.profileService.followUser(CurUser.id, this.userId).subscribe(result => {
                 this.followed = result;
                 this.loading = false;
            });
        } else{
            let CurUser = this.userService.getUser() as UserBasicInfo;
            this.profileService.unfollowUser(CurUser.id, this.userId).subscribe(result => {
                 this.followed = !result;
                 this.loading = false;
            });
        }
    }
}
