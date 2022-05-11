import { Component, Input } from '@angular/core';
import { Repository } from "../../../../models/repository";
import { Translation } from "../../../../models/quran/translation";


@Component({
  selector: "translation",
  templateUrl: "translation.component.html"
})
export class TranslationComponent {
  _curSura: number = 0;
  _curAya: number = 0;
  aya: Translation;
  
  @Input()
  get curSura(): number { return this._curSura; }
  set curSura(value: number) {
    this._curSura = value;
    this.aya = this.repo.translation.filter(q => q.sura == this.curSura)[this.curAya - 1];
  }

  @Input()
  get curAya(): number { return this._curAya; }
  set curAya(value: number) {
    this._curAya = value;
    this.aya = this.repo.translation.filter(q => q.sura == this.curSura )[this.curAya - 1];
  }

  constructor(private repo: Repository) { 
    this.aya = this.repo.translation.filter(q => q.sura == this.curSura)[this.curAya - 1];
  }
}
