import {Component } from "@angular/core";
import { AuthenticationService, IExternalScheme } from "../abstractions/services/authenticationService";
import { identityPaths } from "../constants/authentication.constants";

@Component({
    selector: 'externalSignUps',
  templateUrl: "externalSignUps.component.html"
})

export class ExternalSignUpsComponent {

  externalSchemes!: IExternalScheme[];
  externalSignUpPath: string = identityPaths.SignupWithExternalProvider;

  constructor(public authService: AuthenticationService) {

    this.authService.getExternalSchema().subscribe(schemas => {
                this.externalSchemes = schemas;
              });
          
    }

}
