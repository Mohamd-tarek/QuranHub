import { Component } from '@angular/core';

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
                ];
}
