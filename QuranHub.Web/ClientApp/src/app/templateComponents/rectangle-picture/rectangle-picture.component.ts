import { Component, Input } from '@angular/core';

@Component({
  selector: "rectangle-picture",
  templateUrl: "rectangle-picture.component.html"
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
