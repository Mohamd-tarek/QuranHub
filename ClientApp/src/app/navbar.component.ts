import { Component } from '@angular/core';
import { AuthenticationService } from './auth/authentication.service';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
})
export class NavbarComponent {
  links :string[] = ["read", "search", "statistics", "notes", "analysis"]
  
  constructor(public authService: AuthenticationService){}

}
