import { Component } from '@angular/core';
import { AuthenticationService } from '../../abstractions/services/authenticationService';
import { UserService } from '../../abstractions/services/userService';
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";

@Component({
  selector: 'mini-user-panel',
  templateUrl: './mini-user-panel.component.html',
  styleUrls: ['./mini-user-panel.component.css']
})

export class MiniUserPanelComponent {

  user: UserBasicInfo | null;
  
  constructor(
    public authService: AuthenticationService,
    public userService: UserService) {
    this.user = userService.getUser();
  }

  logout() {
    this.authService.logout();
    this.userService.deleteUserInfo();
    this.authService.logoutCallback();
  }
   
}