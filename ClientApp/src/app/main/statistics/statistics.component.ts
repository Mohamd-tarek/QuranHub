import { Component } from '@angular/core';
import { State } from 'src/app/models/state';
import { Repository } from "../../models/repository";
import { StateService } from '../../stateService.service';
import { FormControl } from '@angular/forms';
import { skipWhile } from 'rxjs/operators';

@Component({
  selector: "statistics",
  templateUrl: "statistics.component.html"
})
export class StatisticsComponent {

  state: State;
  showLetters: FormControl;
  data: [] = [];
  dataCount: number = 0;
  itemsPerPage: number = 120;
  numOfLinks: number = 0 ; 
  dataLoaded: boolean = false;

  constructor(private repo: Repository, private stateService : StateService) {
    this.state = this.stateService.getValue();
    this.showLetters = new FormControl(this.state.showLetters);

    this.stateService.pipe(skipWhile(newState => newState.currentStatisticsPage != this.state.currentStatisticsPage))
    .subscribe(state =>{
      this.state = state;});

    this.showLetters.valueChanges.subscribe(()=>{
      this.updateState();
      this.updateData();
    })

    this.updateData();
    this.dataLoaded = this.data.length > 0 ;
  }

  updateState(){
      this.state.showLetters = this.showLetters.value;
      this.state.currentStatisticsPage = 1;
      this.stateService.next(this.state);
  }

  updateData(){
      this.data = this.convertMapToArray( this.showLetters.value ?  this.repo.letters : this.repo.words);
      this.dataCount = this.data.length;
      this.numOfLinks = Math.trunc(this.dataCount  / this.itemsPerPage);
  }

  convertMapToArray (mapObject : Map<string, number>): any
  {
      let resultArray: any= [];

      for (let entry of mapObject.entries()) {
        let mapKey = entry[0];
        let mapValue = entry[1];
        resultArray.push([mapKey, mapValue]);
      }

    return resultArray;
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
 
}
