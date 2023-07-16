import { Component, OnDestroy } from '@angular/core';
import { QuranRepository } from "../../abstractions/repositories/quranRepository";
import { Sura } from '../../models/meta/sura.model';
import { StateService } from '../../abstractions/services/stateService';
import { skipWhile } from 'rxjs/operators';
import { Quran } from 'src/app/models/quran/quran.model';
import {Subscription } from "rxjs";

@Component({
  selector: "tafseer",
  templateUrl: "tafseer.component.html"
})
export class TafseerComponent implements OnDestroy {

  subscription: Subscription;
  currentTafseerSura :number = 0;
  currentTafseerAya :number = 0; 
  currentTafseer: string = "muyassar" 
  aya!: Quran;
  tafseer!: Quran;
  dataLoaded :boolean = false;
  tafseers!:any;
  
  constructor(private repo: QuranRepository, private stateService : StateService ) {
    this.tafseers = {"muyassar" : this.repo.muyassar,
                     "jalalayn" : this.repo.jalalayn,
                     "tabary" : this.repo.tabary,
                     "qortobi" : this.repo.qortobi,
                     "ibnKatheer" :this.repo.ibnKatheer,
                     "translation": this.repo.translation};
  
   this.subscription = this.stateService.pipe(skipWhile((newState:any)  => this.checkLocalStateChange(newState)))
      .subscribe((newState:any) => {
         this.setState(newState)
      });
  
    this.repo.suras.subscribe((data:any) => {
      this.dataLoaded = data.length > 1 ;
    });
   // this.subscribeKeyboardEvent();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  checkLocalStateChange(newState: any) : boolean{
    return ( newState["currentTafseerSura"] == this.curSura &&
             newState["currentTafseerAya"]  == this.curAya && 
             newState["currentTafseerTafseer"] == this.curTafseer);
  }

  setState(newState: any) : void{
   this.currentTafseerSura = newState["currentTafseerSura"];
   this.currentTafseerAya = newState["currentTafseerAya"];
   this.currentTafseer = newState["currentTafseerTafseer"];
   this.getAya();
   this.getTafseer();
 }
   
  getTafseers():any{
    return  Object.keys(this.tafseers);
  }

  get curSura(): number {
   return this.currentTafseerSura;
  }

  set curSura(value : number) {
    this.currentTafseerSura = value;
    let state: any  = {"currentTafseerSura" : this.currentTafseerSura }
    this.stateService.next(state);
    this.curAya = 1;
  }

  get curAya(): number {
    return this.currentTafseerAya;
   }
 
  set curAya(value : number) {
    this.currentTafseerAya = value;
    let state: any  = {"currentTafseerAya": this.currentTafseerAya}
    this.stateService.next(state);
    this.getAya();
    this.getTafseer();
  }

  get curTafseer(): string {
    return this.currentTafseer;
   }
 
  set curTafseer(value : string) {
    this.currentTafseer = value;
    let state: any  = {"currentTafseerTafseer" : this.currentTafseer }
    this.stateService.next(state);
    this.getTafseer();
  }

  getAya(): void{
    this.repo.quran.subscribe((data:any) => { 
      if(data.length > 1){
        this.aya = 
          data[this.currentTafseerSura][this.currentTafseerAya - 1];
      }
   });
  }

  getTafseer():void{
    this.tafseers[this.currentTafseer].subscribe( (data:any) => {
      if(data.length > 1){
      this.tafseer = data[this.currentTafseerSura][this.currentTafseerAya - 1];
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

//   subscribeKeyboardEvent(){
//       this.nextAyaEvent();
//       this.prevAyaEvent();
//       this.nextTafseerEvent();
//       this.prevTafseerEvent();
//   }

//   nextAyaEvent(){
//   let tafseerComp = this;
//     document.addEventListener('keydown', function(event) {
//       if (event.code == 'ArrowRight') {
//          tafseerComp.next();
//       } 
//     });
//   }

//   prevAyaEvent(){
//     let tafseerComp = this;
//       document.addEventListener('keydown', function(event) {
//         if (event.code == 'ArrowLeft') {
//            tafseerComp.prev();           
//         }
//       });
//   }

//   nextTafseerEvent(){
//       document.addEventListener('keydown', function(event) {
//         if (event.code == 'ArrowUp') {
//           let selectElement : any =  document.getElementById("tafseer") ;
//           selectElement.selectedIndex--;
//           if(selectElement.selectedIndex < 0){
//             selectElement.selectedIndex = 0;
//           }
//           let event = new Event("change");
//           selectElement.dispatchEvent(event);
//         } 
//       });
//   }
  
//   prevTafseerEvent(){
//       document.addEventListener('keydown', function(event) {
//         if (event.code == 'ArrowDown') {
//           let selectElement : any =  document.getElementById("tafseer") ;
//           selectElement.selectedIndex++;
//           if(selectElement.selectedIndex == -1){
//             selectElement.selectedIndex = 5;
//           }
//           let event = new Event("change");
//           selectElement.dispatchEvent(event);
//         } 
//       });
//   }

 }
