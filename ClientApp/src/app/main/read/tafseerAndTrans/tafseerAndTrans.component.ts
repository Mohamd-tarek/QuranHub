import { Component } from '@angular/core';
import { Sura } from '../../../models/meta/sura';
import { Repository } from "../../../models/repository";
import { StateSevice } from '../../stateService.service';

@Component({
  selector: "tafseerAndTrans",
  templateUrl: "tafseerAndTrans.component.html"
})
export class TafseerAndTransComponent {
  _curSura : number = 1;
  _curAya : number = 1;
  dataLoaded :boolean;
  constructor(private repo: Repository, private state : StateSevice ) {
    this.curSura = this.state.currentSura;
    this.curAya = this.state.currentAya;
    this.repo.translation;
    this.repo.tafseer;
    this.repo.suras;


    this.dataLoaded = this.repo.tafseer.length > 1 && 
                      this.repo.translation.length > 1
  }

  get curSura(): number {
   return this._curSura;
  }

  set curSura(value : number) {
     this.state.currentSura = value;
     this._curSura = value;
   }

   get curAya(): number {
    return this._curAya;
   }
 
   set curAya(value : number) {
      this.state.currentAya = value;
      this._curAya = value;
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
}
