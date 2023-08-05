import { Component } from "@angular/core";
import { UserService } from "../abstractions/services/userService";
import { PrivacySetting } from "../models/user/privacySetting.model";

@Component({
    selector: 'editPrivacySetting',
  templateUrl: "editPrivacySetting.component.html"
})

export class EditPrivacySettingComponent {

  privacySetting!: PrivacySetting;

  dataLoaded: boolean = false;

  loading: boolean = false;

  message: string = "";

  showMessage: boolean = false;

  constructor(public userService: UserService) {
    this.getPrivacySetting();
    }

  getPrivacySetting() {
    
    let user = this.userService.getPrivacySetting().subscribe(setting => {
      this.privacySetting = setting;
      console.log(setting)
      this.dataLoaded = true;
    })
  }

  submit() {
    this.loading = true;
    this.userService.editPrivacySetting(this.privacySetting).subscribe(response => {
      this.loading = false;
      this.message = "edited successfully";
      this.showMessage = true;
    }, (err) => {
      this.loading = false;
      this.message = "en error occured";
      this.showMessage = true;
    })
  }
}
