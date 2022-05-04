import { Component, Input } from '@angular/core';
import { Repository } from "../../../models/repository";
import { Quran } from "../../../models/quran/quran";

@Component({
  selector: "quran",
  templateUrl: "quran.component.html"
})
export class QuranComponent {
  _curSelection: number = 0;
  sura: Quran[] = [];
  @Input()
  get curSelection(): number { return this._curSelection; }
  set curSelection(curSelection: number) {
    this._curSelection = curSelection;
    this.sura = this.repo.quran.filter(q => q.sura == this.curSelection);
    
    
  }

  constructor(private repo: Repository) {  
  }

}
