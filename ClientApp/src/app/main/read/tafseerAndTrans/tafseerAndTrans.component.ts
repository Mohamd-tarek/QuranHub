import { Component } from '@angular/core';
import { Sura } from '../../../models/meta/sura';
import { Repository } from "../../../models/repository";
import { StateSevice } from '../../stateService.service';
import { State } from 'src/app/models/state';

@Component({
  selector: "tafseerAndTrans",
  templateUrl: "tafseerAndTrans.component.html"
})
export class TafseerAndTransComponent {
 
  state : State;
  dataLoaded :boolean;
   constructor(private repo: Repository, private stateService : StateSevice ) {
  
    this.repo.translation;
    this.repo.tafseer;
    this.repo.suras;
    this.state = stateService.getValue();


    this.dataLoaded = this.repo.suras.length > 1 &&
                      this.repo.tafseer.length > 1 &&
                      this.repo.translation.length > 1;
  }

  get curSura(): number {
   return this.state.currentTafseerAndTranSura;
  }

  set curSura(value : number) {
     this.state.currentTafseerAndTranSura = value;
     this.stateService.next(this.state);
   }

   get curAya(): number {
    return this.state.currentTafseerAndTranAya;
   }
 
   set curAya(value : number) {
      this.state.currentTafseerAndTranAya = value;
      this.stateService.next(this.state);
      
    }

  get suras(): Sura[] {
    return this.repo.suras;
  }

  get ayas(): number[] {
    let ayas = [];
    for(let i = 1; i <= this.repo.suras[this.curSura - 1].ayas; ++i){
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
