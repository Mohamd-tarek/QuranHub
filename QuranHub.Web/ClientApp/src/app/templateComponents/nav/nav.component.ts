import { Component, Input } from '@angular/core';

@Component({
  selector: 'Nav',
  templateUrl: './Nav.component.html',
})

export class NavComponent {

  @Input()
  links! :any[];
  
  constructor(){}

}
