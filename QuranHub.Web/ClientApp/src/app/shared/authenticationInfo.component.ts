import { Component } from '@angular/core';
import { AuthenticationService  } from '../abstractions/services/authenticationService';
import { UserService } from '../abstractions/services/userService';

@Component({
  selector: 'authenticationInfo',
  templateUrl: './authenticationInfo.component.html',
})

export class AuthenticationInfoComponent {

  showUserPanel:boolean = false;

  constructor(
    public authService: AuthenticationService,
    public userService: UserService) {
  }

  togglePanel(event:any) {
    event.stopPropagation();
    this.showUserPanel = !this.showUserPanel;
  }
  
  hidePanel(event:any) {
    this.showUserPanel = false;
  }
   
}
