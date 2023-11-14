import { Component, Input } from '@angular/core';

@Component({
  selector: "anchor-list",
  templateUrl: "anchor-list.component.html"
})

export class AnchorListComponent {
  @Input()
  links! :any[]; 
  
  constructor() {}
}
