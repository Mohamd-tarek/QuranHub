import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: "shareCount",
  templateUrl: "shareCount.component.html"
})

export class ShareCountComponent {

  @Input()
  count!: number;

  @Output()
  showSharesEvent = new EventEmitter();

  showShares() {
    this.showSharesEvent.emit();
  }
 
}
