import { Component, Input } from '@angular/core';

@Component({
  selector: 'navbar',
  templateUrl: './navbar.component.html',
})

export class NavbarComponent  {

  @Input()
  links! :any[]; 
  
}
