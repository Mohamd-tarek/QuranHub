import {Component } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { AuthenticationService } from "../../abstractions/services/authenticationService";

@Component({
    selector: 'recoverPasswordConfirm',
    templateUrl: "recoverPasswordConfirm.component.html"
})

export class RecoverPasswordConfirmComponent {

    showConfirmedMessage: boolean = false;
    email!: string;
    token!: string;
    newPassword!: string;
    confirmPassword!: string;
    loading: boolean = false;
    showError: boolean = false;

    constructor(public authService: AuthenticationService, activeRoute: ActivatedRoute ) {
        this.email = activeRoute.snapshot.params["email"];
        this.token = activeRoute.snapshot.params["token"];
    }

    recoverPasswordConfirm() {
        this.showError = false;
        this.loading = true;
        this.authService.recoverPasswordConfirm(this.email, this.token,
                     this.newPassword, this.confirmPassword).subscribe(result => {
            this.loading = false;
            this.showConfirmedMessage = !result;
        });
    }
}
