import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: "next-pointer",
  templateUrl: "next-pointer.component.html"
})

export class NextPointerComponent {

  @Output()
  nextEvent = new EventEmitter();

  nextEventStart() {
    this.nextEvent.emit();
  }
  
}
