import { Component, OnDestroy } from '@angular/core';
import { QuranRepository } from "../../abstractions/repositories/quranRepository";
import { StateService } from '../../abstractions/services/stateService';
import { skipWhile } from 'rxjs/operators'
import {Subscription } from "rxjs";

@Component({
  selector: "mindMap",
  templateUrl: "mindMap.component.html"
})

export class MindMapComponent implements OnDestroy{
  
  subscription: Subscription;
  dataLoaded : boolean = false;
  currentMindMap : number = 0;
  currentMindMapSura: number = 0;
    
  constructor(private repo: QuranRepository, private stateService: StateService) { 
    
    this.subscription = this.stateService.pipe(skipWhile((newState:any) => this.checkLocalStateChange(newState)))
    .subscribe((newState:any) =>{
                this.setState(newState);
                });      
      this.repo.suras.subscribe((d:any) => this.dataLoaded = d.length > 1);
  }
           
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  checkLocalStateChange(newState: any) : boolean{
    return (newState["currentMindMapSura"] == this.currentMindMapSura);
  }
  
  setState(newState: any):void{
    this.currentMindMapSura = newState["currentMindMapSura"];
    this.updateCurrentMindMap(this.currentMindMapSura);
    this.updateSura();
  }

  updateSura(){
    this.repo.getMindMap(this.currentMindMap).subscribe(data => {
      document.getElementById("mindmap")?.setAttribute("src","data:image/png;base64," + data ) ;
      
    })
  }
              
  updateCurrentMindMap(value: number): void{
      if(value < 57){
        this.currentMindMap =  value - 1;
      }
      else if(value < 67){
        this.currentMindMap = 57;
      }
      else if(value < 78){
        this.currentMindMap = 58;
      }
      else if(value < 93){
        this.currentMindMap = 59;
      }
      else{
        this.currentMindMap = 60;
      }
  }

  get curSura(): number { return this.currentMindMapSura; }
  
  set curSura(value: number) {
    this.currentMindMapSura = value;
    this.updateCurrentMindMap(value);
    let state: any  = {"currentMindMapSura": this.currentMindMapSura };
    this.stateService.next(state);    
  }

  get suras(){
    return this.repo.suras.getValue();
  }
  
  next(){
    if(this.curSura < 115){
      this.curSura++;
    }
  }

  prev(){
    if(this.curSura > 1 ){
      this.curSura--;
    }
  }

}
