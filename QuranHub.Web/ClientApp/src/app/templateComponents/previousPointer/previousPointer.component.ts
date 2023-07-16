import { Component, Input, EventEmitter, Output } from '@angular/core';

@Component({
  selector: "previousPointer",
  templateUrl: "previousPointer.component.html"
})

export class PreviousPointerComponent {

  @Output()
  previousEvent = new EventEmitter();

  previousEventStart() {
    this.previousEvent.emit();

  }
  
}
