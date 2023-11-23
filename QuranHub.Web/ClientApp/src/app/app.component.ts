import { Component } from '@angular/core';
import {TranslateService} from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'QuranHub';

  links :any[] = [{name :"read", url : "/read"},
                  {name :"tafseer", url : "/tafseer"},
                  {name :"mindMaps", url : "/mindMaps"},
                  {name :"search", url : "/search"},
                  {name :"notes", url : "/notes"},
                  {name :"statistics", url : "/statistics"},
                  {name :"analysis", url : "/analysis"},
                  {name :"documentary", url : "/documentary"},
                ];
  constructor(translate: TranslateService) {
    translate.addLangs(['en', 'ar']);
    translate.setDefaultLang('ar');
    translate.currentLang = "ar";

  }
}
