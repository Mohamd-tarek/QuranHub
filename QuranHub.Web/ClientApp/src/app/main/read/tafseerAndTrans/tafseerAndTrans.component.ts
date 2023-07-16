import { Component, OnDestroy } from '@angular/core';
import { Sura } from '../../../models/meta/sura.model';
import { QuranRepository } from "../../../abstractions/repositories/quranRepository";
import { StateService } from '../../../abstractions/services/stateService';
import { skipWhile } from 'rxjs/operators';
import { Quran } from 'src/app/models/quran/quran.model';
import {Subscription } from "rxjs";

@Component({
  selector: "tafseerAndTrans",
  templateUrl: "tafseerAndTrans.component.html"
})

export class TafseerAndTransComponent implements OnDestroy  {

  subscription: Subscription;
  currentTafseerAndTranSura :number = 0;
  currentTafseerAndTranAya :number = 0; 
  dataLoaded :boolean = false;
  aya!: Quran;
  tafseer!: Quran;
  translation!: Quran;

  constructor(private repo: QuranRepository, private stateService : StateService ) {
  
    this.subscription = this.stateService.pipe(skipWhile((newState:any)  => this.checkLocalStateChange(newState)))
      .subscribe((newState:any) => {
          this.setState(newState);
        });
  
    this.repo.suras.subscribe((data:any) => {
      this.dataLoaded = data.length > 1 ;
    });
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
  
  checkLocalStateChange(newState: any) : boolean{
    return ( newState["currentTafseerAndTranAya"]  == this.curAya &&
             newState["currentTafseerAndTranSura"] == this.curSura);
  }

  setState(newState: any):void{
   this.currentTafseerAndTranSura = newState["currentTafseerAndTranSura"];
   this.currentTafseerAndTranAya = newState["currentTafseerAndTranAya"];
   this.updateData();
  }
   
  updateData(): void{
    this.repo.quran.subscribe((data: any) => this.aya = this.chooseData(data));
    this.repo.muyassar.subscribe((data: any) => this.tafseer = this.chooseData(data));
    this.repo.translation.subscribe((data:any) => this.translation = this.chooseData(data));      
  }

  chooseData(data :any): any{ 
    return data.length > 0 ? data[this.curSura][this.curAya - 1] : null;
  }

  get curSura(): number {
   return this.currentTafseerAndTranSura;
  }

  set curSura(value : number) {
    this.currentTafseerAndTranSura = value;
    let state: any  = {"currentTafseerAndTranSura" : this.currentTafseerAndTranSura };
    this.stateService.next(state);
    this.curAya = 1;
   }

  get curAya(): number {
    return this.currentTafseerAndTranAya;
  }
 
   set curAya(value : number) {
      this.currentTafseerAndTranAya = value;
      let state: any  = {"currentTafseerAndTranAya": this.currentTafseerAndTranAya}
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
    if(this.curAya > 1 ){
      this.curAya--;
    }
    else if(this.curSura > 1){
      this.curSura--;
      this.curAya = 1;
    }
  }
}
