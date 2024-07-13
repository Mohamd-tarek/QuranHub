import { Component } from '@angular/core';
import {TranslateService} from '@ngx-translate/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent {
  title = 'QuranHub';

  links :any[] = [{name :"READ", url : "/read"},
                  {name :"RECITAL", url : "/recital"},
                  {name :"TAFSEER", url : "/tafseer"},
                  {name :"MINDMAPS", url : "/mindMaps"},
                  {name :"SEARCH", url : "/search"},
                  {name :"HADITH", url : "/hadith"},
                  {name :"NOTES", url : "/notes"},
                  {name :"STATISTICS", url : "/statistics"},
                  {name :"ANALYSIS", url : "/analysis"},
                  {name :"DOCUMENTARY", url : "/documentary"},
                ];
  constructor(public translate: TranslateService) {
    translate.addLangs(['en', 'ar']);
    translate.setDefaultLang('en');
    translate.currentLang = "en";

  }
}
