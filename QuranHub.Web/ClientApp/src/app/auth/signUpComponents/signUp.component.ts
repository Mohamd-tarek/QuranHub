import {Component } from "@angular/core";
import { AuthenticationService } from "../../abstractions/services/authenticationService";
import { ActivatedRoute } from "@angular/router";
import { UserService } from "../../abstractions/services/userService";

@Component({
    selector: 'signUp',
    templateUrl: "signUp.component.html"
})

export class SignUpComponent {
  message: string = "";
  showMessage: boolean = false;
  loading: boolean = false;
  externalSignUpCallback!: string;
    
  constructor(
    public authService: AuthenticationService,
    public userService: UserService,
    private activeRoute: ActivatedRoute) {
    this.externalSignUpCallback = this.activeRoute.snapshot.params["SignUpExternalCallback"];

      if (this.externalSignUpCallback) {
        this.signUpExternalCallback();
      }
    }


  signUp() {
    this.showMessage = false;
    this.loading = true;
    this.authService.signup().subscribe(result => {
      console.log(result);
      this.message = "signup success confirm your account";
      this.showMessage = true;
      this.loading = false;
    },
      err => {
        this.showMessage = true;
        this.message = err.error.message;
        this.loading = false;
      });
  }

  signUpExternalCallback() {
    this.showMessage = false;
    this.loading = true;
    this.authService.signupExternalCallback().subscribe(response => {
      if (response.success) {
        this.message = "signin success geting user info";
        this.authService.storeAuthenticationToken(response.token);
        this.userService.getUserInfo();
      }
      this.authService.signupCallback();
      this.loading = false;
    },
      err => {
        console.log(err);
        this.showMessage = true;
        this.message = err.error.message;
        this.loading = false;
      });
  }
}


