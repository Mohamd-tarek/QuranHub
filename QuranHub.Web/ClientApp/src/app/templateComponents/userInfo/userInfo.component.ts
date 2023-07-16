import { Component, Input } from '@angular/core';

@Component({
  selector: "userInfo",
  templateUrl: "userInfo.component.html"
})

export class UserInfoComponent {

  @Input()
  user!: any;

  @Input()
  profilePictureDiamter!: number
}
