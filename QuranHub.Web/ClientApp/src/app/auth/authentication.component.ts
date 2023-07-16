import { Component } from "@angular/core";
import { AuthenticationService } from "../abstractions/services/authenticationService";

@Component({
    templateUrl: "authentication.component.html"
})

export class AuthenticationComponent {

    links :any[] = [{name :"login Info", url : "./loginInfo"},
                    {name :"edit login Info", url : "./editLoginInfo"},
                    {name :"change password", url : "./changePassword"},
                    {name :"about", url : "./aboutInfo"},
                    {name :"delete", url : "./deleteAccount"}]  

    constructor(public authService: AuthenticationService) {  }


}
