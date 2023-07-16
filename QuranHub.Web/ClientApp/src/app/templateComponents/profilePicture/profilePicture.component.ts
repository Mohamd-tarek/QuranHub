import { Component, Input } from '@angular/core';

@Component({
  selector: "profilePicture",
  templateUrl: "profilePicture.component.html"
})
export class ProfilePictureComponent {

  @Input()
  diamter!: number;

  @Input()
  picture!: any;
  
  constructor() {}

}
