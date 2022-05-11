import { Component } from '@angular/core';
import { Repository } from "../../../models/repository";
import { Quran } from "../../../models/quran/quran";
import { StateSevice } from '../../stateService.service';
@Component({
  selector: "quran",
  templateUrl: "quran.component.html"
})
export class QuranComponent {
  _curSura: number = 1;
  dataLoaded : boolean;
  sura: Quran[] = [];
  
  get curSura(): number { return this._curSura; }

  set curSura(value: number) {
    this._curSura = value;
    this.state.currentSura = value;
    this.sura = this.repo.quran.filter(q => q.sura == this.curSura);
    
    
  }

  constructor(private repo: Repository, private state: StateSevice) {  
    this.repo.quran;
    this.curSura = this.state.currentSura;
    this.dataLoaded = this.repo.quran.length > 1 ;

  }
  get suras(){
    return this.repo.suras;
  }

}
