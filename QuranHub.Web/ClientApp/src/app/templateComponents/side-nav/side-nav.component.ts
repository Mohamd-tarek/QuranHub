import { Component, Input } from '@angular/core';

@Component({
  selector: 'side-nav',
  templateUrl: './side-nav.component.html',
})

export class SideNavComponent {

  @Input()
  links! :any[];

  constructor(){}

}
