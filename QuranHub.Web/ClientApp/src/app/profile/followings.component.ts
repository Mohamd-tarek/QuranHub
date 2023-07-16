import { Component, Input } from '@angular/core';
import { UserBasicInfo } from "../models/user/userBasicInfo.model";

@Component({
  selector: "followings",
  templateUrl: "followings.component.html"
})

export class FollowingsComponent {

  @Input()
  followings!: UserBasicInfo[];

  @Input()
  userId!: string;
 
}
