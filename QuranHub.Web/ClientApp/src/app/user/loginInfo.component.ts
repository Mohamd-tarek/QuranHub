import { Component } from "@angular/core";
import { UserService } from "../abstractions/services/userService";
import { UserBasicInfo } from "../models/user/userBasicInfo.model";

@Component({
    selector: 'loginInfo',
    templateUrl: "loginInfo.component.html"
})

export class LoginInfoComponent {

    email!:string;
    username!:string;

  constructor(public userService: UserService) {
        this.getUser();
    }

  getUser() {
    let user = this.userService.getUser() as UserBasicInfo;

    this.email = user.email ?? "no data";
    this.username = user.userName ?? "no data";

    }
}
