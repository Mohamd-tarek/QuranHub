import { Component, Input } from '@angular/core';

@Component({
  selector: "user-Info",
  templateUrl: "user-Info.component.html"
})

export class UserInfoComponent {

  @Input()
  user!: any;

  @Input()
  profilePictureDiamter!: number
}
