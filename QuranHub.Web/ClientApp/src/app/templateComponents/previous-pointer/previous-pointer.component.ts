import { Component, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: "previous-pointer",
  templateUrl: "previous-pointer.component.html"
})

export class PreviousPointerComponent {

  @Output()
  previousEvent = new EventEmitter();

  previousEventStart() {
    this.previousEvent.emit();

  }
  
}
