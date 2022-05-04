import { Component, Input } from '@angular/core';
import { Repository } from "../../../models/repository";
import { Translation } from "../../../models/quran/translation";


@Component({
  selector: "translation",
  templateUrl: "translation.component.html"
})
export class TranslationComponent {
  _curSelection: number = 0;
  sura: Translation[] = [];

  @Input()
  get curSelection(): number { return this._curSelection; }
  set curSelection(curSelection: number) {
    this._curSelection = curSelection;
    this.sura = this.repo.translation.filter(q => q.sura == this.curSelection);
  }


  constructor(private repo: Repository) { 
    
  }

}
