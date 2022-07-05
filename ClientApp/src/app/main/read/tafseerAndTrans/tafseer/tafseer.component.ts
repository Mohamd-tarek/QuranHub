import { Component } from '@angular/core';
import { Repository } from "../../../../models/repository";
import { Tafseer } from '../../../../models/quran/tafseer';
import { StateService } from 'src/app/stateService.service';
import { skipWhile } from 'rxjs/operators';

@Component({
  selector: "tafseer",
  templateUrl: "tafseer.component.html"
})
export class TafseerComponent {
  currentTafseerAndTranSura :number = 1;
  currentTafseerAndTranAya :number = 1;  
  aya!: Tafseer;
  
  constructor(private repo: Repository,private stateService : StateService) { 
    this.updateCurrentAya();

    stateService.pipe(skipWhile(newState  => this.checkLocalStateChange(newState)))
    .subscribe(newState => {
      this.setInitialState(newState);
    });
  }

  checkLocalStateChange(newState: any) : boolean{
    return ( newState["currentTafseerAndTranAya"]  == this.currentAya() &&
             newState["currentTafseerAndTranSura"] == this.currentSura()); 
  }

  setInitialState(newState: any): void{
    this.currentTafseerAndTranSura = newState["currentTafseerAndTranSura"];
    this.currentTafseerAndTranAya = newState["currentTafseerAndTranAya"];
    this.updateCurrentAya();
 }
   
  updateCurrentAya(): void{
    this.repo.tafseer.subscribe(data =>this.aya = this.chooseAya(data));

  }

  chooseAya(data :Tafseer[]): Tafseer{
       return data.filter(q => q.sura == this.currentSura())[this.currentAya() - 1]
  }

  currentSura(): number{
    return this.currentTafseerAndTranSura;
  }

  currentAya(): number{
    return this.currentTafseerAndTranAya;
  }  
}
