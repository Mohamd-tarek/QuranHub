import { Component, Input } from '@angular/core';

@Component({
  selector: "profile-picture",
  templateUrl: "profile-picture.component.html"
})
export class ProfilePictureComponent   {

  @Input()
  diamter!: number;

  @Input()
  picture!: any;
  
  constructor() {}



}
