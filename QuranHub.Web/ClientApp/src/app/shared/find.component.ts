import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'find',
  templateUrl: './find.component.html',
})

export class FindComponent  {
  
  constructor(public router: Router){
    this.router.routeReuseStrategy.shouldReuseRoute = function () {
    return false;
    }; 

  }

  searchQuery:string= "";
 
  submitQuery(){
     this.router.navigateByUrl(`/searchResults/all/${this.searchQuery}`);
     this.searchQuery = "";
  }
}
