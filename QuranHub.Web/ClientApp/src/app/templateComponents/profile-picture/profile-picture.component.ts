import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: "profile-picture",
  templateUrl: "profile-picture.component.html"
})
export class ProfilePictureComponent implements OnInit  {

  @Input()
  diamter!: number;

  @Input()
  picture!: any;
  
  constructor() {}

  ngOnInit(): void {
    console.log(this.picture.length);
  }

}
