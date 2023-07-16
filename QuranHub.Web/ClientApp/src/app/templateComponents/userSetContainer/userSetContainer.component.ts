import { Component, Input } from '@angular/core';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";

@Component({
  selector: "userSetContainer",
  templateUrl: "userSetContainer.component.html"
})

export class UserSetContainerComponent {

  @Input()
  users!: UserBasicInfo[];

}
