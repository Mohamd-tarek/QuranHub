import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: "nextPointer",
  templateUrl: "nextPointer.component.html"
})

export class NextPointerComponent {

  @Output()
  nextEvent = new EventEmitter();

  nextEventStart() {
    this.nextEvent.emit();
  }
  
}
