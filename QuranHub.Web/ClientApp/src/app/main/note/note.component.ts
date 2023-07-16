import { Component, OnDestroy } from '@angular/core';
import { Sura } from '../../models/meta/sura.model';
import { QuranRepository } from "../../abstractions/repositories/quranRepository";
import { StateService } from '../../abstractions/services/stateService';
import { skipWhile } from 'rxjs/operators';
import {Subscription } from "rxjs";

@Component({
  selector: "note",
  templateUrl: "note.component.html"
})

export class NoteComponent implements OnDestroy  {
  subscription: Subscription;
  currentNoteSura: number = 1;
  currentNoteAya: number = 1;
  dataLoaded : boolean = false;
  
  constructor(private repo: QuranRepository, private stateService : StateService ) {
    this.subscription =  stateService.pipe(skipWhile(newState => this.checkLocalStateChange(newState)))
    .subscribe((newState:any) => {
        this.setState(newState);
      });

    this.repo.suras.subscribe((data:any) =>{
      this.dataLoaded = data.length > 1;
    });

  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  checkLocalStateChange(newState: any) : boolean{
    return ( newState["currentNoteSura"]  == this.currentNoteSura &&
             newState["currentNoteAya"] == this.currentNoteAya); 
  }

  setState(newState: any):void{
    this.currentNoteSura = newState["currentNoteSura"];
    this.currentNoteAya = newState["currentNoteAya"];
  }

  get curSura(): number {
   return this.currentNoteSura;
  }

  set curSura(value : number) {
    this.currentNoteSura = value;
    let state: any  = {"currentNoteSura": this.currentNoteSura}
    this.stateService.next(state);
    this.curAya = 1;
   }

   get curAya(): number {
    return this.currentNoteAya;
   }
 
   set curAya(value : number) {
    this.currentNoteAya = value;
    let state: any = {"currentNoteAya": this.currentNoteAya}
    this.stateService.next(state);   
   }

  get suras(): Sura[] {
    return this.repo.suras.getValue();
  }

  get ayas(): number[] {
    let ayas = [];
    for (let i = 1; i <= this.suras[this.curSura - 1].ayas; ++i){
      ayas.push(i);
    }
    return ayas;
  }

  next(){
    if(this.curAya < this.ayas.length){
      this.curAya++;
    }
    else if(this.curSura < 115){
      this.curSura++;
      this.curAya = 1;
    }
  }

  prev(){
    if (this.curAya > 1 ){
      this.curAya--;
    }
    else if (this.curSura > 1){
      this.curSura--;
      this.curAya = 1;
    }
  }

}
