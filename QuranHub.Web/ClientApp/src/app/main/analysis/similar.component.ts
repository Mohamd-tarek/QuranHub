import { Component, OnDestroy } from '@angular/core';
import { QuranRepository } from "../../abstractions/repositories/quranRepository";
import { Sura } from '../../models/meta/sura.model';
import { StateService } from '../../abstractions/services/stateService';
import { skipWhile } from 'rxjs/operators';
import { Quran } from 'src/app/models/quran/quran.model';
import {Subscription } from "rxjs";

@Component({
  selector: "Similar",
  templateUrl: "similar.component.html"
})

export class SimilarComponent implements OnDestroy {
  
  subscription: Subscription;
  currentSimilarSura :number = 0;
  currentSimilarAya :number = 0; 
  aya!: Quran;
  suraLoaded :boolean = false;
  ayaLoaded :boolean = false;
  result: Quran[] = [];
    
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
    return ( newState["currentSimilarAya"]  == this.currentSimilarAya &&
             newState["currentSimilarSura"] == this.currentSimilarSura);
  }

  setState(newState: any):void{
        this.currentSimilarSura = newState["currentSimilarSura"];
        this.currentSimilarAya  = newState["currentSimilarAya"];
        this.getAya();
  }

  get dataLoaded() :boolean{
      return (this.suraLoaded && this.ayaLoaded)
  }
                      
  get curSura(): number {
   return this.currentSimilarSura;
  }

  set curSura(value : number) {
    this.currentSimilarSura = value;
    let state: any  = {"currentSimilarSura" : this.currentSimilarSura }
    this.stateService.next(state);
    this.curAya = 1;
  }

  get curAya(): number {
    return this.currentSimilarAya;
   }
 
  set curAya(value : number) {
    this.currentSimilarAya = value;
    let state: any  = {"currentSimilarAya": this.currentSimilarAya}
    this.stateService.next(state);
    this.getAya();
    
  }

  getAya(): void{
    this.repo.quran.subscribe((data:any) => { 
      if(data.length > 1){
        this.ayaLoaded = true;
        this.aya = 
          data[this.currentSimilarSura][this.currentSimilarAya - 1];
          this.repo.getSimilarAyas(this.aya).subscribe( data => this.result = data);

      }
   });
  }

  get suras(): Sura[] {
    return this.repo.suras.getValue();
  }

  get ayas(): number[] {
    let ayas = [];
    for(let i = 1; i <= this.suras[this.curSura - 1].ayas; ++i){
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
