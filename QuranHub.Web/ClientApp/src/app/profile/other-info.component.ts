import { Component } from '@angular/core';

@Component({
  selector: "other-info",
  templateUrl: "other-info.component.html"
})

export class OtherInfoComponent  {

  links :any[] = [{name :"PROFILE_DETAILS", url : "./profileDetails"},
                  {name :"ABOUT", url : "./about"},
                  {name :"FOLLOWERS", url : "./followers"},
                  {name :"FOLLOWINGS", url : "./followings"},
                ]  

 
}
