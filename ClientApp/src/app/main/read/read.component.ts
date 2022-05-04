import { Component } from '@angular/core';
import { Sura } from '../../models/meta/sura';
import { Repository } from "../../models/repository";
import { StateSevice } from '../stateService.service';

@Component({
  selector: "read",
  templateUrl: "read.component.html"
})
export class ReadComponent {
  _curSelection : number;
  dataLoaded :boolean;
  constructor(private repo: Repository, private state : StateSevice ) {
    this._curSelection = this.state.currentSura;
    this.repo.translation;
    this.repo.tafseer;
    this.repo.quran;


    this.dataLoaded = this.repo.quran.length > 1 &&
                      this.repo.tafseer.length > 1 && 
                      this.repo.translation.length > 1
  }

  get curSelection(): number {
   return this._curSelection;
  }

  set curSelection(value : number) {
     this.state.currentSura = value;
     this._curSelection = value;
   }

  get suras(): Sura[] {
    return this.repo.suras;
  }
}
