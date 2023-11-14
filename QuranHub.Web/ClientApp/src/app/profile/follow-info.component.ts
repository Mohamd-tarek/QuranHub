import { Component, Input } from '@angular/core';

@Component({
  selector: "follow-info",
  templateUrl: "follow-info.component.html"
})

export class FollowInfoComponent  {

  constructor(){}

  @Input()
  user!: any;
 
}
