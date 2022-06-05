import { Component, Input } from '@angular/core';
import { Repository } from "../../../../models/repository";
import { Tafseer } from '../../../../models/quran/tafseer';
import { State } from 'src/app/models/state';
import { StateService } from 'src/app/stateService.service';
import { skipWhile } from 'rxjs/operators';

@Component({
  selector: "tafseer",
  templateUrl: "tafseer.component.html"
})
export class TafseerComponent {
  state : State;
  aya: Tafseer;
  
  constructor(private repo: Repository,private stateService : StateService) { 
    this.state = this.stateService.getValue();
    this.aya = this.repo.tafseer.filter(q => q.sura == this.state.currentTafseerAndTranSura)[this.state.currentTafseerAndTranAya - 1];

    stateService.pipe(skipWhile(newState => newState.currentTafseerAndTranAya != this.state.currentTafseerAndTranAya || newState.currentTafseerAndTranSura != this.state.currentTafseerAndTranSura))
    .subscribe(newState => {
      this.state = newState;
      this.aya = this.repo.tafseer.filter(q => q.sura == this.state.currentTafseerAndTranSura)[this.state.currentTafseerAndTranAya - 1];
    })
  }

  
}
