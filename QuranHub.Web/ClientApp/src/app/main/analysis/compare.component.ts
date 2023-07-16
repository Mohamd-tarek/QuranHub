import { Component, OnDestroy } from '@angular/core';
import { QuranRepository } from "../../abstractions/repositories/quranRepository";
import { Sura } from '../../models/meta/sura.model';
import { StateService } from '../../abstractions/services/stateService';
import { skipWhile } from 'rxjs/operators';
import { Quran } from 'src/app/models/quran/quran.model';
import { Subscription } from "rxjs";

@Component({
  selector: "Comapre",
  templateUrl: "compare.component.html"
})

export class CompareComponent implements OnDestroy {
  
  subscription: Subscription;
  currentCompareSura :number = 1;
  currentComapreAya :number = 1; 
  suraLoaded :boolean = false;
  ayaLoaded :boolean = false;
  compareSetInfo: any = [];
  compareSet: Quran[] = [];
  
  constructor(private repo: QuranRepository, private stateService : StateService ) {
    this.subscription = stateService.pipe(skipWhile((newState:any)  => this.checkLocalStateChange(newState)))
    .subscribe((newState:any) => {
         this.setState(newState);
      });
    
    this.repo.suras.subscribe((data:any) => {
      this.suraLoaded = data.length > 1;
    });
  }
  
  ngOnDestroy() {
    this.subscription.unsubscribe();
  } 

  checkLocalStateChange(newState: any) : boolean{
    return JSON.stringify(newState)==JSON.stringify(this.curCompareSetInfo);
  }

  setState(newState: any):void{
    this.compareSetInfo = newState["compareSetInfo"];
    this.setCompareSet();
  }

  setCompareSet(){
    this.repo.quran.subscribe((data:any) => { 
      this.compareSet = [];

      if (data.length > 1){

        for (let i = 0; i < this.compareSetInfo.length; ++i) {
          let aya = data[this.compareSetInfo[i][0]][this.compareSetInfo[i][1] - 1];
          this.compareSet.push(aya);
        }
        this.ayaLoaded = true;
      }
   });

  }

  get dataLoaded() :boolean{
    return (this.suraLoaded && this.ayaLoaded);
}

  get curCompareSetInfo(): any {
    return this.compareSetInfo;
   }
 
  set curCompareSetInfo(value : any) {
    this.compareSetInfo = value;
    let state: any  = {"compareSetInfo": this.compareSetInfo}
    this.stateService.next(state);    
  }

  get curSura(): number {
   return this.currentCompareSura;
  }

  set curSura(value : number) {
    this.currentCompareSura = value;
  }

  get curAya(): number {
    return this.currentComapreAya;
   }
 
  set curAya(value : number) {
    this.currentComapreAya = value;
  }

  get suras(): Sura[] {
    return this.repo.suras.getValue();
  
  }

  get currentSuraAyas(): number[] {
    let ayas = [];
    for(let i = 1; i <= this.suras[this.curSura - 1].ayas; ++i){
      ayas.push(i);
    }
    return ayas;
  }

  addAya(): void{
    let aya = [this.curSura, this.curAya];
    let cur = this.curCompareSetInfo;
    cur.push(aya);
    this.curCompareSetInfo = cur;
  }

  removeAya(aya: Quran): void{
    let compareSetInfoPos = 0;
    this.compareSetInfo.forEach((element:any, index:any) => {
         if(element[0] == aya.sura && element[1] == aya.aya)
         {
          compareSetInfoPos = index;
         }
    });
    let cur = this.curCompareSetInfo;
    cur.splice(compareSetInfoPos, 1);
    this.curCompareSetInfo = cur;
  }
 
}
