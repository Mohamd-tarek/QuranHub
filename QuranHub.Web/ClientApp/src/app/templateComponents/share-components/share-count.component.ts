import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: "share-count",
  templateUrl: "share-count.component.html"
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
