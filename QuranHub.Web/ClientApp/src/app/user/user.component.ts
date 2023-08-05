import { Component } from "@angular/core";
import { UserService } from "../abstractions/services/userService";
import { UserBasicInfo } from "../models/user/userBasicInfo.model";

@Component({
    templateUrl: "user.component.html"
})

export class UserComponent {

    done:boolean = true;
    user: UserBasicInfo;

    links :any[] = [{name :"login Info", url : "./loginInfo"},
                    {name :"edit login Info", url : "./editLoginInfo"},
                    {name :"change password", url : "./changePassword"},
                    {name: "about", url: "./aboutInfo" },
                    {name: "edit privacy", url: "./editPrivacy" },
                    {name :"delete", url : "./deleteAccount"}]  

  constructor(public userService: UserService) {
    this.user = userService.getUser() as UserBasicInfo;
    }
}
