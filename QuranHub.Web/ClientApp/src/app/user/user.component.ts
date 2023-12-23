import { Component } from "@angular/core";
import { UserService } from "../abstractions/services/userService";
import { UserBasicInfo } from "../models/user/userBasicInfo.model";

@Component({
    templateUrl: "user.component.html"
})

export class UserComponent {

    done:boolean = true;
    user: UserBasicInfo;

    links :any[] = [{name :"LOGIN_INFO", url : "./loginInfo"},
                    {name :"EDIT_LOGIN_INFO", url : "./editLoginInfo"},
                    {name :"CHANGE_PASSWORD", url : "./changePassword"},
                    {name: "ABOUT", url: "./aboutInfo" },
                    {name: "EDIT_PRIVACY", url: "./editPrivacy" },
                    {name :"DELETE", url : "./deleteAccount"}, 
                    {name :"LANGUAGE_SETTING", url : "./languageSetting"}]  

  constructor(public userService: UserService) {
    this.user = userService.getUser() as UserBasicInfo;
    }
}
