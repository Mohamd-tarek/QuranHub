import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';


@Component({
  selector: "modal",
  templateUrl: "modal.component.html"
})

export class ModalComponent  {

  @Input()
  title!: string;

  @Output()
  hideModalEvent = new EventEmitter();
  
  hideModal(){
    this.hideModalEvent.emit();
  }
}
