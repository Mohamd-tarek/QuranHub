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
  aya!: Tafseer;
  
  constructor(private repo: Repository,private stateService : StateService) { 
    this.state = this.stateService.getValue();
    this.updateCurrentAya();

    stateService.pipe(skipWhile((newState :State) => this.checkLocalStateChange(newState)))
    .subscribe(newState => {
      this.state = newState;
      this.updateCurrentAya();})
  }

  checkLocalStateChange(newState: State) : boolean{
   return ( newState.currentTafseerAndTranAya  != this.currentAya() ||
            newState.currentTafseerAndTranSura != this.currentSura());  }
  
  updateCurrentAya(): void{
    this.repo.tafseer.subscribe(data =>this.aya = this.chooseAya(data));

  }

  chooseAya(data :Tafseer[]): Tafseer{
       return data.filter(q => q.sura == this.currentSura())[this.currentAya() - 1]
  }

  currentSura(): number{
    return this.state.currentTafseerAndTranSura;
  }

  currentAya(): number{
    return this.state.currentTafseerAndTranAya;
  }

  
}
