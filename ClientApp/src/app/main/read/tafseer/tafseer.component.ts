import { Component, Input } from '@angular/core';
import { Repository } from "../../../models/repository";
import { Tafseer } from "../../../models/quran/tafseer";

@Component({
  selector: "tafseer",
  templateUrl: "tafseer.component.html"
})
export class TafseerComponent {
  _curSelection: number = 0;
  sura: Tafseer[] = [];

  @Input()
  get curSelection(): number { return this._curSelection; }
  set curSelection(curSelection: number) {
    this._curSelection = curSelection;
    this.sura = this.repo.tafseer.filter(q => q.sura == this.curSelection);
  }

  constructor(private repo: Repository) { 
  }

  
}
