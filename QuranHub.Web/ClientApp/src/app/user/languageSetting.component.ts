import {Component } from "@angular/core";
import { UserService } from "../abstractions/services/userService";
import { TranslateService } from '@ngx-translate/core';


@Component({
    selector: 'language-setting',
    templateUrl: "languageSetting.component.html"
})

export class LanguageSettingComponent {

  constructor(public translate: TranslateService) { }

}
