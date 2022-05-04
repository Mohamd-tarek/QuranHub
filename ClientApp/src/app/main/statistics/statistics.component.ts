import { Component } from '@angular/core';
import { Repository } from "../../models/repository";
import { StateSevice } from '../stateService.service';

@Component({
  selector: "statistics",
  templateUrl: "statistics.component.html"
})
export class StatisticsComponent {

  data : [] = [];
  dataCount : number = 0;
  itemsPerPage : number = 120;
  currentPage : number;
  numOfLinks :number = 0 ; 
  dataLoaded : boolean;

  constructor(private repo: Repository, private state : StateSevice) {
    this.data = this.mapToArray(repo.words);
    this.dataCount = this.data.length;
    this.numOfLinks = Math.trunc(this.dataCount  / this.itemsPerPage);
    this.currentPage = this.state.currentStatisticsPage;
    this.dataLoaded = this.data.length > 0 ;

    
  }

  mapToArray (mapObject : Map<string, number>): any
  {
      let result: any= [];

      for (let entry of mapObject.entries()) {
        let mapKey = entry[0];
        let mapValue = entry[1];
        result.push([mapKey, mapValue]);
      }
     result =  result.sort((a: any[], b: any[])=> { 
       let comp = (c: any, d: any)=> {c-d}
         return comp(a[1], b[1]);
      });
    return result;
  }

  getElements():any {
    
      let pageOfData : any = [];
      let startIndex = (this.currentPage  - 1) * this.itemsPerPage ;
      let endIndex = this.currentPage  *  this.itemsPerPage;
      let size = this.dataCount;

     while( startIndex < size && startIndex < endIndex)
     {
      pageOfData.push(this.data[startIndex]);
       ++startIndex;
     }

    return pageOfData;
  }

  onPageChange(page : number)
  {
    this.currentPage = page;
    this.state.currentStatisticsPage = page;
  }
 
}
