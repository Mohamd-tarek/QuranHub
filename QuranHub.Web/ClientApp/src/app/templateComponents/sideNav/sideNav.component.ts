import { Component, Input } from '@angular/core';

@Component({
  selector: 'sideNav',
  templateUrl: './sideNav.component.html',
})

export class SideNavComponent {

  @Input()
  links! :any[];

  constructor(){}

}
