import { Component, Input } from '@angular/core';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";

@Component({
  selector: "user-set-container",
  templateUrl: "user-set-container.component.html"
})

export class UserSetContainerComponent {

  @Input()
  users!: UserBasicInfo[];

}
