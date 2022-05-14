import { Component, Input } from '@angular/core';
import { Repository } from "../../../../models/repository";
import { Translation } from "../../../../models/quran/translation";
import { StateSevice } from 'src/app/main/stateService.service';
import { State } from 'src/app/models/state';
import { skipWhile } from 'rxjs/operators';


@Component({
  selector: "translation",
  templateUrl: "translation.component.html"
})
export class TranslationComponent {
 
  state : State;
  aya: Translation;
  
  constructor(private repo: Repository,private stateService : StateSevice) { 
    this.state = this.stateService.getValue();
    this.aya = this.repo.translation.filter(q => q.sura == this.state.currentTafseerAndTranSura)[this.state.currentTafseerAndTranAya - 1];

    stateService.pipe(skipWhile(newState => newState.currentTafseerAndTranAya != this.state.currentTafseerAndTranAya || newState.currentTafseerAndTranSura != this.state.currentTafseerAndTranSura))
    .subscribe(newState => {
      this.state = newState;
      this.aya = this.repo.translation.filter(q => q.sura == this.state.currentTafseerAndTranSura)[this.state.currentTafseerAndTranAya - 1];
    })
    
  }
}
