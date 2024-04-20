import { Component, Input, inject } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ProfilePictureModalComponent } from '../profile-picture-modal/profile-picture-modal.component';

@Component({
  selector: "user-Info",
  templateUrl: "user-Info.component.html"
})

export class UserInfoComponent {

  private modalService = inject(NgbModal);
  @Input()
  user!: any;

  @Input()
  profilePictureDiamter!: number

  @Input()
  profilePictureClickable:boolean = false;

  showProilePictureModal(){
    if(this.profilePictureClickable)
      {
        const modalRef = this.modalService.open(ProfilePictureModalComponent);
        modalRef.componentInstance.picture = this.user?.profilePicture;
      }
  }
}
