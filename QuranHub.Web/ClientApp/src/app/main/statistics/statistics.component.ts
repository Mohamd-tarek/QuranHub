import { Component, OnDestroy } from '@angular/core';
import { QuranRepository } from "../../abstractions/repositories/quranRepository";
import { StateService } from '../../abstractions/services/stateService';
import { FormControl } from '@angular/forms';
import { skipWhile } from 'rxjs/operators';
import {Subscription } from "rxjs";

@Component({
  selector: "statistics",
  templateUrl: "statistics.component.html"
})

export class StatisticsComponent implements OnDestroy {

  subscription: Subscription;
  currentStatisticsPage : number = 1;
  showLetters: FormControl = new FormControl();
  data: [] = [];
  dataCount: number = 0;
  itemsPerPage: number = 120;
  numOfLinks: number = 0 ; 
  dataLoaded: boolean = false;

  constructor(private repo: QuranRepository, private stateService : StateService) {
    this.subscription = this.stateService.pipe(skipWhile(newState => this.checkLocalStateChange(newState)))
    .subscribe((newState:any) =>{
       this.setState(newState)
    });

    this.showLetters.valueChanges.subscribe(()=>{
      this.updateState();
      this.updateData();
    })

    this.repo.words.subscribe((data:any) =>{
      this.updateData();
      this.dataLoaded = (data.size > 1)}
    );
      
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  checkLocalStateChange(newState: any) : boolean{
    return ( newState["showLetters"]  == this.showLetters.value &&
             newState["currentStatisticsPage"] == this.currentStatisticsPage);
  }

  setState(newState: any) : void{
    this.showLetters.setValue(newState["showLetters"], {emitEvent :false});
    this.currentStatisticsPage = newState["currentStatisticsPage"];
  }
  
  updateState(){
    let state: any = {"showLetters": this.showLetters.value }
    this.stateService.next(state);
  }

  updateData(){
    this.data = this.convertMapToArray( this.showLetters.value ?  this.repo.letters.getValue() : this.repo.words.getValue());
    this.dataCount = this.data.length;
    this.numOfLinks = Math.trunc(this.dataCount  / this.itemsPerPage);
  }

  navigateEvent(value:number){
    let state: any = {"currentStatisticsPage": value }
    this.stateService.next(state);
  }
  

  convertMapToArray (mapObject : Map<string, number>): any {
      let resultArray: any= [];

      for (let entry of mapObject.entries()) {
        let mapKey = entry[0];
        let mapValue = entry[1];
        resultArray.push([mapKey, mapValue]);
      }
      resultArray.sort((a:any, b:any)=>  b[1] - a[1]);
    return resultArray;
  }

  getData():any {
    let pageOfData : any = [];
    let startIndex = this.showLetters.value ? 1 :  (this.currentStatisticsPage  - 1) * this.itemsPerPage ;
    let endIndex = this.currentStatisticsPage  *  this.itemsPerPage;
    let size = this.dataCount;

    while( startIndex < size && startIndex < endIndex) {
    pageOfData.push(this.data[startIndex][0] + ' : '  + this.data[startIndex][1]);
    ++startIndex;
    }

    return pageOfData;
  }
 
}
