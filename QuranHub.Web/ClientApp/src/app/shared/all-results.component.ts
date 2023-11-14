import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'all-results',
  templateUrl: './all-results.component.html',
})

export class AllResultsComponent  {
  
  query!: string;

  constructor(private activeRoute: ActivatedRoute ){

    this.activeRoute.params.subscribe( params => {
      this.query = params["q"];
      });
  }
 
}
