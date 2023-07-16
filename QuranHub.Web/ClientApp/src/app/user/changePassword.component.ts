import {Component } from "@angular/core";
import { UserService } from "../abstractions/services/userService";

@Component({
    selector: 'changePassword',
    templateUrl: "changePassword.component.html"
})

export class ChangePasswordComponent {

  current!:string 
  newPassword!:string
  confirmPassword!: string;
  message!:string;
  loading:boolean = false;
    
  constructor(public userService: UserService) {}

    changePassword() {
        this.loading = true;
      this.userService.changePassword(this.current, this.newPassword, this.confirmPassword).subscribe(result => {
            this.message = "password changed"
            this.loading = false;          
      },
        err => {
          this.message = err.error;
          this.loading = false;
        });
    }
}
