import { Component } from '@angular/core';
import { Repository } from "../../../models/repository";
import { Quran } from "../../../models/quran/quran";
import { StateService } from '../../../stateService.service';
import { skipWhile } from 'rxjs/operators';
import { State } from 'src/app/models/state';
@Component({
  selector: "quran",
  templateUrl: "quran.component.html"
})
export class QuranComponent {
  
  dataLoaded : boolean;
  sura: Quran[] = [];
  state : State;
  
  get curSura(): number { return this.state.currentQuranSura; }

  set curSura(value: number) {
    this.state.currentQuranSura = value;
    this.stateService.next(this.state);    
    
  }

  constructor(private repo: Repository, private stateService: StateService) {  
    this.repo.quran;
    this.state = this.stateService.getValue();
     this.stateService.pipe(skipWhile(newState => newState.currentQuranSura != this.curSura))
               .subscribe(state =>{
                 this.sura = this.repo.quran.filter(q => q.sura == this.curSura)});

    this.dataLoaded = this.repo.quran.length > 1 ;

  }

  get suras(){
    return this.repo.suras;
  }
  
  next(){
    if(this.curSura < 115){
          this.curSura++;
    }
  }

  prev(){
    if(this.curSura > 1 ){
          this.curSura--;
    }
  }

}
