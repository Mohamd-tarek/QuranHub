import { Component, Input } from '@angular/core';

@Component({
  selector: "anchorList",
  templateUrl: "anchorList.component.html"
})

export class AnchorListComponent {
  @Input()
  links! :any[]; 
  
  constructor() {}
}
