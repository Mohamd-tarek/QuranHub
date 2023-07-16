import { Component, Input } from '@angular/core';

@Component({
  selector: "shareCount",
  templateUrl: "shareCount.component.html"
})

export class ShareCountComponent {

  @Input()
  count!: number;
 
}
