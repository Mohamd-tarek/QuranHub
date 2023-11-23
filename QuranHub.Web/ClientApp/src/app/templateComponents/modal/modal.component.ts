import { Component, Input, Output, EventEmitter, OnInit, inject } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';



@Component({
  selector: "modal",
  templateUrl: "modal.component.html"
})

export class ModalComponent  {

  @Input()
  title!: string;

  activeModal = inject(NgbActiveModal);

  @Output()
  hideModalEvent = new EventEmitter();
  
  hideModal(){
    this.hideModalEvent.emit();
    this.activeModal.dismiss('Cross click');
  }
}
