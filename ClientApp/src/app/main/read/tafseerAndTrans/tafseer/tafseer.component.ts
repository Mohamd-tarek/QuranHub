import { Component, Input } from '@angular/core';
import { Repository } from "../../../../models/repository";
import { Tafseer } from '../../../../models/quran/tafseer';

@Component({
  selector: "tafseer",
  templateUrl: "tafseer.component.html"
})
export class TafseerComponent {
  _curSura: number = 0;
  _curAya: number = 0;
  aya: Tafseer;
  @Input()
  get curSura(): number { return this._curSura; }
  set curSura(value: number) {
    this._curSura = value;
    this.aya = this.repo.tafseer.filter(q => q.sura == this.curSura)[this.curAya - 1];
  }

  @Input()
  get curAya(): number { return this._curAya; }
  set curAya(value: number) {
    this._curAya = value;
    this.aya = this.repo.tafseer.filter(q => q.sura == this.curSura )[this.curAya - 1];
  }

  constructor(private repo: Repository) { 
    this.aya = this.repo.tafseer.filter(q => q.sura == this.curSura )[this.curAya - 1];
  }

  
}
