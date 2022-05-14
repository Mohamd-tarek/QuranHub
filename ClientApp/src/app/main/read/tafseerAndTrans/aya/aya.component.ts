import { Component, Input } from '@angular/core';
import { Repository } from "../../../../models/repository";
import { Quran } from '../../../../models/quran/quran';
import { State } from 'src/app/models/state';
import { skipWhile } from 'rxjs/operators';
import { StateSevice } from 'src/app/main/stateService.service';

@Component({
  selector: "aya",
  templateUrl: "aya.component.html"
})
export class AyaComponent {
  
  state : State;
  aya: Quran;
  
  constructor(private repo: Repository,private stateService : StateSevice) { 
    this.state = this.stateService.getValue();
    this.aya = this.repo.quran.filter(q => q.sura == this.state.currentTafseerAndTranSura)[this.state.currentTafseerAndTranAya - 1];

    stateService.pipe(skipWhile(newState => newState.currentTafseerAndTranAya != this.state.currentTafseerAndTranAya || newState.currentTafseerAndTranSura != this.state.currentTafseerAndTranSura))
    .subscribe(newState => {
      this.state = newState;
      this.aya = this.repo.quran.filter(q => q.sura == this.state.currentTafseerAndTranSura)[this.state.currentTafseerAndTranAya - 1];
    })
  }

  
}
