import { Component, Input, inject } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: "profile-picture-modal",
  templateUrl: "profile-picture-modal.component.html"
})
export class ProfilePictureModalComponent   {


  @Input()
  picture!: any;
  
  constructor() {}


}
