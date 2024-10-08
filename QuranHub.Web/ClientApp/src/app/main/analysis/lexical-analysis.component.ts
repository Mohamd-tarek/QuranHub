import { Component, OnDestroy } from '@angular/core';
import { QuranRepository } from "../../abstractions/repositories/quranRepository";
import { Sura } from '../../models/meta/sura.model';
import { StateService } from '../../abstractions/services/stateService';
import { skipWhile } from 'rxjs/operators';
import { Quran } from 'src/app/models/quran/quran.model';
import {Subscription } from "rxjs";

@Component({
  selector: "lexical-analysis",
  templateUrl: "lexical-analysis.component.html"
})

export class LexicalAnalysisComponent implements OnDestroy {
  
  subscription: Subscription;
  currentLexicalSura :number = 0;
  currentLexicalAya :number = 0; 
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
    return ( newState["currentLexicalAya"]  == this.currentLexicalAya &&
             newState["currentLexicalSura"] == this.currentLexicalSura);
  }

  setState(newState: any):void{
        this.currentLexicalSura = newState["currentLexicalSura"];
        this.currentLexicalAya  = newState["currentLexicalAya"];
        this.getAya();
  }

  get dataLoaded() :boolean{
      return (this.suraLoaded && this.ayaLoaded)
  }
                      
  get curSura(): number {
   return this.currentLexicalSura;
  }

  set curSura(value : number) {
    this.currentLexicalSura = value;
    let state: any  = {"currentLexicalSura" : this.currentLexicalSura }
    this.stateService.next(state);
    this.curAya = 1;
  }

  get curAya(): number {
    return this.currentLexicalAya;
   }
 
  set curAya(value : number) {
    this.currentLexicalAya = value;
    let state: any  = {"currentLexicalAya": this.currentLexicalAya}
    this.stateService.next(state);
    this.getAya();
    
  }

  getAya(): void{
    this.repo.quran.subscribe((data:any) => { 
      if(data.length > 1){
        this.ayaLoaded = true;
        this.aya = 
          data[this.currentLexicalSura][this.currentLexicalAya - 1];
          //this.repo.getSimilarAyas(this.aya).subscribe( data => this.result = data);

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
