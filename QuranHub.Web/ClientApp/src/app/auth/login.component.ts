import {Component } from "@angular/core";
import { AuthenticationService } from "../abstractions/services/authenticationService";
import { ActivatedRoute } from "@angular/router";
import { UserService } from "../abstractions/services/userService";

@Component({
    selector: 'login',
    templateUrl: "login.component.html"
})

export class LoginComponent {

    message:string = "";
    externalLoginCallback!:string;  
    showMessage: boolean = false;
    loading: boolean = false;

  constructor(
    public authService: AuthenticationService,
    public userService: UserService,
    private activeRoute: ActivatedRoute) {
              
              this.externalLoginCallback = this.activeRoute.snapshot.params["LoginExternalCallback"];

              if(this.externalLoginCallback){
                this.loginExternalCallback();
              }
    }

    login() {
        this.showMessage = false;
        this.loading = true;
        this.authService.login().subscribe(response => {
          if (response.success) {
            this.message = "signing in success getting user information";
            this.authService.storeAuthenticationToken(response.token);
            this.userService.getUserInfo();
            this.authService.loginCallback();
            this.loading = false;
          } else {
            this.showMessage = true;
            this.message = "invalid username or password";
            this.loading = false;
          }
        });
    }

    loginExternalCallback() {
        this.showMessage = false;
        this.loading = true;
        this.message = "user info accuired and processing signing in";
        this.authService.loginExternalCallback().subscribe(response => {
          if (response.success) {
              this.message = "signing in success getting user information";
              this.authService.storeAuthenticationToken(response.token);
              this.userService.getUserInfo();
              this.authService.loginCallback();
              this.loading = false;
          } else {
            this.showMessage = true;
            this.message = "invalid login credintial";
            this.loading = false;
          }
        });
    }
}
