import { Component, Input } from '@angular/core';
import { Repository } from "../../../../models/repository";
import { Quran } from '../../../../models/quran/quran';

@Component({
  selector: "aya",
  templateUrl: "aya.component.html"
})
export class AyaComponent {
  _curSura: number = 0;
  _curAya: number = 0;
  aya: Quran;
  @Input()
  get curSura(): number { return this._curSura; }
  set curSura(value: number) {
    this._curSura = value;
    this.aya = this.repo.quran.filter(q => q.sura == this.curSura)[this.curAya - 1];
  }

  @Input()
  get curAya(): number { return this._curAya; }
  set curAya(value: number) {
    this._curAya = value;
    this.aya = this.repo.quran.filter(q => q.sura == this.curSura )[this.curAya - 1];
  }

  constructor(private repo: Repository) { 
    this.aya = this.repo.quran.filter(q => q.sura == this.curSura )[this.curAya - 1];
  }

  
}
