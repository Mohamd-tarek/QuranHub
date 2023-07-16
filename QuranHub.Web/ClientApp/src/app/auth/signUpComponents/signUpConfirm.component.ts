import {Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { AuthenticationService } from "../../abstractions/services/authenticationService";

@Component({
    selector: 'signUpConfirm',
    templateUrl: "signUpConfirm.component.html"
})

export class SignUpConfirmComponent {

    confirmEnded: boolean = false;
    message: string = "verifing account please wait .......";
    verified:boolean = false;
    resended: boolean = false;
    email!: string;
    token!: string;

  constructor(public authService: AuthenticationService, activeRoute: ActivatedRoute) {
        this.email = activeRoute.snapshot.queryParams["email"];
        this.token = activeRoute.snapshot.queryParams["token"];
        console.log(this.email, + " , " + this.token);
        this.signUpConfirm();
    }

  signUpConfirm() {
        
        this.authService.signupConfirm(this.email, this.token).subscribe(result => {
          this.message = "Account confirmed";
          this.verified = true;
          this.confirmEnded = true;
        },
          err => {
            this.verified = false;
            this.message = err.error.message;
            this.confirmEnded = true;
          });
    }

    resend(){
        this.authService.signupResend(this.email).subscribe(result => {
            this.resended = result;
        });
    }
    
}
