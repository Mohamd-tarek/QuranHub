import { Component, Input } from '@angular/core';
import { UserBasicInfo } from "../models/user/userBasicInfo.model";

@Component({
  selector: "followers",
  templateUrl: "followers.component.html"
})

export class FollowersComponent {

  @Input()
  followers!: UserBasicInfo[];

  @Input()
  userId!: string;
}
