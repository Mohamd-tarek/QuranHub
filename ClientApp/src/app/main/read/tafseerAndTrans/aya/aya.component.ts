import { Component } from '@angular/core';
import { Repository } from "../../../../models/repository";
import { Quran } from '../../../../models/quran/quran';
import { skipWhile } from 'rxjs/operators';
import { StateService } from 'src/app/stateService.service';

@Component({
  selector: "aya",
  templateUrl: "aya.component.html"
})
export class AyaComponent {
  currentTafseerAndTranSura :number = 0;
  currentTafseerAndTranAya :number = 0;  
  aya!: Quran;
  
  constructor(private repo: Repository,private stateService : StateService) { 
    this.updateCurrentAya();

    stateService.pipe(skipWhile(newState => this.checkLocalStateChange(newState)))
    .subscribe(newState => {
      this.setInitialState(newState);
    })
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
    this.repo.quran.subscribe(data =>this.aya = this.chooseAya(data));

  }

  chooseAya(data :Quran[]): Quran{
       return data.filter(q => q.sura == this.currentSura())[this.currentAya() - 1]
  }

  currentSura(): number{
    return this.currentTafseerAndTranSura;
  }

  currentAya(): number{
    return this.currentTafseerAndTranAya;
  }
}
