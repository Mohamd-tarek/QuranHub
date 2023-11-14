import { Component, Input } from '@angular/core';

@Component({
  selector: "link-element",
  templateUrl: "link-element.component.html"
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
