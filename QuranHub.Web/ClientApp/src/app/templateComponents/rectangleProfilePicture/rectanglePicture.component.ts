import { Component, Input } from '@angular/core';

@Component({
  selector: "rectanglePicture",
  templateUrl: "rectanglePicture.component.html"
})
export class RectanglePictureComponent {

  @Input()
  width!: number;

  @Input()
  height!: number;

  @Input()
  picture!: any;
  
  constructor() {}

}
