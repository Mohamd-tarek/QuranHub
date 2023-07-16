import { Component, Input } from '@angular/core';
import { ProfileService } from '../abstractions/services/profileService';
import { ActivatedRoute, Router } from '@angular/router';
import { Gender, IAboutInfo, Religion } from '../abstractions/services/userService';

@Component({
  selector: "about",
  templateUrl: "about.component.html"
})

export class AboutComponent {

  userId!:string;
  aboutInfo!:IAboutInfo;
  dataLoaded: boolean = false;
  
  constructor(
    public profileServeice: ProfileService,
    private activeRoute: ActivatedRoute) {
    this.userId = this.activeRoute.parent?.snapshot.params["userId"];

    this.profileServeice.getAboutInfo(this.userId).subscribe(info => {
      this.aboutInfo = info;
      this.dataLoaded = true;
    });
 
  }
  
  getGenderName(enumValue: number | string) : string{
     return this.getEnumKeyByEnumValue(Gender, enumValue);
  }

  getReligionName(enumValue: number | string) : string{
    return this.getEnumKeyByEnumValue(Religion, enumValue);
 }

  getEnumKeyByEnumValue(myEnum: any, enumValue: number | string): string {
    let keys = Object.keys(myEnum).filter((x) => myEnum[x] == enumValue);
    return keys.length > 0 ? keys[0] : '';
  }

}
