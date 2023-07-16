import {Component } from "@angular/core";
import { UserService } from "../abstractions/services/userService";
import { UserBasicInfo } from "../models/user/userBasicInfo.model";
import { UserFormModel } from "./userFormModel";

@Component({
    selector: 'editLoginInfo',
    templateUrl: "editLoginInfo.component.html"
})

export class EditLoginInfoComponent {
    userForm!: UserFormModel;
    dataLoaded:boolean = false;
    email!:string;
    username!:string;
    message: string = "";
    showMessage: boolean = false;
    loading: boolean = false;

  constructor(public userService: UserService) {
        this.getUser();
    }

  getUser() {
    let user = this.userService.getUser() as UserBasicInfo;
    
    this.email = user.email;
    this.username = user.userName;
    this.userForm = new UserFormModel(this.email, this.username);
    this.dataLoaded = true;
    }
   
    submitForm() {
        if (this.userForm.valid){
            this.loading = true;
            this.email = this.userForm.controls["email"].value;
            this.username = this.userForm.controls["userName"].value;
            this.userService.editUserData(this.email, this.username).subscribe(result => {
              this.message = "profile updated";
              this.showMessage = true;
              this.loading = false;
              this.updateUser();
              this.userForm.reset();
               
            },
              err => { 
                this.message = err.error;
                this.showMessage = true;
                this.loading = false;
              });
        }
    }

  updateUser() {
    let user = this.userService.getUser() as UserBasicInfo;
    user.email = this.email
    user.userName = this.username;
    this.userService.updateUser(user);
  }
}
