import { Component, Input } from '@angular/core';

@Component({
  selector: "followInfo",
  templateUrl: "followInfo.component.html"
})

export class FollowInfoComponent  {

  constructor(){}

  @Input()
  user!: any;
 
}
