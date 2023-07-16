import { Component } from '@angular/core';

@Component({
  selector: "otherInfo",
  templateUrl: "otherInfo.component.html"
})

export class OtherInfoComponent  {

  links :any[] = [{name :"profile details", url : "./profileDetails"},
                  {name :"about", url : "./about"},
                  {name :"followers", url : "./followers"},
                  {name :"followings", url : "./followings"},
                ]  

 
}
