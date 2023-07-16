import {Component } from "@angular/core";
import { UserService } from "../abstractions/services/userService";

@Component({
    selector: 'delete',
    templateUrl: "deleteAccount.component.html"
})

export class DeleteAccountComponent {

  constructor(public userService: UserService) { }

    deleteAccount() {
      this.userService.deleteAccount();
    }
}
