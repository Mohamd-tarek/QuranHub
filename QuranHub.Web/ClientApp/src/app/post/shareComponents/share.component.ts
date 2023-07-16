import { Component, Input, Output, EventEmitter } from '@angular/core';

@Component({
  selector: "share",
  templateUrl: "share.component.html"
})

export class ShareComponent {
 
  @Output() shareStartEvent = new EventEmitter();

  onShare(){
    this.shareStartEvent.emit();
  }
}
