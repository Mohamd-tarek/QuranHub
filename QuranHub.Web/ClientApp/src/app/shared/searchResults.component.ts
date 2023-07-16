import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'searchResults',
  templateUrl: './searchResults.component.html',
})

export class SearchResultsComponent  {

  links :any[] = [{name :"All", url : "all"},
                  {name :"Posts", url : "posts"},
                  {name :"People", url : "people"},
                ]; 

  query!: string;

  constructor( private activeRoute: ActivatedRoute) {
    this.activeRoute.firstChild?.params.subscribe( params => {
    
      this.query = params["q"];
      this.links.forEach(ele => {
       ele.url = ele.url + '/' + this.query;
      });
    }); 
 
  }
}
