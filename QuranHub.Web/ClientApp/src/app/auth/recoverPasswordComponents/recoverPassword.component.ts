import {Component } from "@angular/core";
import { AuthenticationService } from "../../abstractions/services/authenticationService";

@Component({
    selector: 'recoverPassword',
    templateUrl: "recoverPassword.component.html"
})

export class RecoverPasswordComponent {
 
    email!:string 
    showError: boolean = false;
    emailSent: boolean = false;
    loading: boolean = false;
    
    constructor(public authService: AuthenticationService) {}

    recoverPassword() {
        this.showError = false;
        this.loading = true;
        this.authService.recoverPassword(this.email).subscribe(result => {
            this.loading = false;
            this.showError = !result;
            this.emailSent = result;
        });
    }
}
