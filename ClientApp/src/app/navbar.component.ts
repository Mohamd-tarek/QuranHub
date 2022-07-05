import { Component } from '@angular/core';
import { AuthenticationService } from './auth/authentication.service';
import { StateService } from './stateService.service';
import { skipWhile } from 'rxjs/operators';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
})
export class NavbarComponent {
  links :string[] = ["read", "search", "statistics", "notes", "analysis"]
  authenticated: boolean = false;
  
  constructor(public stateService: StateService,public authService : AuthenticationService){
    this.stateService.pipe(skipWhile(newState => newState["authenticated"] != this.authenticated))
    .subscribe(newState =>{
      this.authenticated = newState["authenticated"];
    });

  }

}
