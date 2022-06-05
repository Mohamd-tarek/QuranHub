import { Component } from '@angular/core';
import { AuthenticationService } from './auth/authentication.service';
import { State } from './models/state';
import { StateService } from './stateService.service';
import { skipWhile } from 'rxjs/operators';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
})
export class NavbarComponent {
  links :string[] = ["read", "search", "statistics", "notes", "analysis"]
  state: State;
  
  constructor(public stateService: StateService,public authService : AuthenticationService){
    this.state = this.stateService.getValue();
    this.stateService.pipe(skipWhile(newState => newState.authenticated != this.state.authenticated))
    .subscribe(state =>{
      this.state = state;
    });

  }

}
