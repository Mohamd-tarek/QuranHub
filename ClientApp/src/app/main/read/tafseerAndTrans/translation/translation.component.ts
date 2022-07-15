import { Component } from '@angular/core';
import { Repository } from "../../../../models/repository";
import { Translation } from "../../../../models/quran/translation";
import { StateService } from 'src/app/stateService.service';
import { skipWhile } from 'rxjs/operators';


@Component({
  selector: "translation",
  templateUrl: "translation.component.html"
})
export class TranslationComponent {
 
  currentTafseerAndTranSura :number = 0;
  currentTafseerAndTranAya :number = 0; 
  aya!: Translation;
  
  constructor(private repo: Repository,private stateService : StateService) { 
    this.updateCurrentAya();

    stateService.pipe(skipWhile(newState => this.checkLocalStateChange(newState)))
    .subscribe(newState => {
      this.currentTafseerAndTranSura = newState["currentTafseerAndTranSura"];
      this.currentTafseerAndTranAya = newState["currentTafseerAndTranAya"];
      this.updateCurrentAya();})
  }

  checkLocalStateChange(newState: any) : boolean{
    return ( newState["currentTafseerAndTranAya"]  == this.currentAya() &&
             newState["currentTafseerAndTranSura"] == this.currentSura());  }
   
  updateCurrentAya(): void{
    this.repo.translation.subscribe(data =>this.aya = this.chooseAya(data));

  }

  chooseAya(data :Translation[]): Translation{
       return data.filter(q => q.sura == this.currentSura())[this.currentAya() - 1]
  }

  currentSura(): number{
    return this.currentTafseerAndTranSura;
  }

  currentAya(): number{
    return this.currentTafseerAndTranAya;
  }
}
