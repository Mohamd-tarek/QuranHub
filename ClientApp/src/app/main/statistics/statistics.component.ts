import { Component } from '@angular/core';
import { State } from 'src/app/models/state';
import { Repository } from "../../models/repository";
import { StateSevice } from '../stateService.service';

@Component({
  selector: "statistics",
  templateUrl: "statistics.component.html"
})
export class StatisticsComponent {

  state : State;
  data : [] = [];
  dataCount : number = 0;
  itemsPerPage : number = 120;
  numOfLinks :number = 0 ; 
  dataLoaded : boolean;

  constructor(private repo: Repository, private stateService : StateSevice) {
    this.state = this.stateService.getValue();
    this.data = this.mapToArray(repo.words);
    this.dataCount = this.data.length;
    this.numOfLinks = Math.trunc(this.dataCount  / this.itemsPerPage);
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
      let startIndex = (this.state.currentStatisticsPage  - 1) * this.itemsPerPage ;
      let endIndex = this.state.currentStatisticsPage  *  this.itemsPerPage;
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
    this.state.currentStatisticsPage = page;
    this.stateService.next(this.state);
  }
 
}
