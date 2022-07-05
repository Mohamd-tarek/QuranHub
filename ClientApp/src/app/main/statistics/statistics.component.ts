import { Component } from '@angular/core';
import { Repository } from "../../models/repository";
import { StateService } from '../../stateService.service';
import { FormControl } from '@angular/forms';
import { skipWhile } from 'rxjs/operators';

@Component({
  selector: "statistics",
  templateUrl: "statistics.component.html"
})
export class StatisticsComponent {

  currentStatisticsPage : number = 1;
  showLetters: FormControl = new FormControl();
  data: [] = [];
  dataCount: number = 0;
  itemsPerPage: number = 120;
  numOfLinks: number = 0 ; 
  dataLoaded: boolean = false;

  constructor(private repo: Repository, private stateService : StateService) {

    this.stateService.pipe(skipWhile(newState => this.checkLocalStateChange(newState)))
    .subscribe(newState =>{
      this.showLetters.setValue(newState["showLetters"], {emitEvent :false});
      this.currentStatisticsPage = newState["currentStatisticsPage"];
      });

    this.showLetters.valueChanges.subscribe(()=>{
      this.updateState();
      this.updateData();
    })

    this.repo.words.subscribe(data =>{
      this.updateData();
      this.dataLoaded = (data.size > 1)}
     );
      
  }

  checkLocalStateChange(newState: any) : boolean{
    return ( newState["showLetters"]  == this.showLetters.value &&
             newState["currentStatisticsPage"] == this.currentStatisticsPage);  }
   

  updateState(){
      let state: any = {"showLetters": this.showLetters.value }
      this.stateService.next(state);
  }

  updateData(){
      
      this.data = this.convertMapToArray( this.showLetters.value ?  this.repo.letters.getValue() : this.repo.words.getValue());
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
      let startIndex = this.showLetters.value ? 1 :  (this.currentStatisticsPage  - 1) * this.itemsPerPage ;
      let endIndex = this.currentStatisticsPage  *  this.itemsPerPage;
      let size = this.dataCount;

     while( startIndex < size && startIndex < endIndex)
     {
      pageOfData.push(this.data[startIndex]);
       ++startIndex;
     }

    return pageOfData;
  }
 
}
