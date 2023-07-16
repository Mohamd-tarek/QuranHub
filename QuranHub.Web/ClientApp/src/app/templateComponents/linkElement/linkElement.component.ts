import { Component, Input } from '@angular/core';

@Component({
  selector: "linkElement",
  templateUrl: "linkElement.component.html"
})
export class LinkElementComponent {

  @Input()
  fontSize!: number;

  @Input()
  linkTarget!: string;

  @Input()
  linkName!: string;

  @Input()
  routeParm!: string;

  constructor() {}

}
