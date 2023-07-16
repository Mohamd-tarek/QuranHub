import {Component } from "@angular/core";
import { AuthenticationService, IExternalScheme } from "../abstractions/services/authenticationService";
import { identityPaths } from "../constants/authentication.constants";

@Component({
    selector: 'externalLogins',
  templateUrl: "externalLogins.component.html"
})

export class ExternalLoginsComponent {

  externalSchemes!: IExternalScheme[];
  externalLoginPath: string = identityPaths.LoginWithExternalProvider;

  constructor(public authService: AuthenticationService) {

    this.authService.getExternalSchema().subscribe(schemas => {
                this.externalSchemes = schemas;
              });
          
    }

}
