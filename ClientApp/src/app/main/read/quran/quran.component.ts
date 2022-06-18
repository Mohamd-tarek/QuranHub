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
  
  dataLoaded : boolean = false;
  sura!: Quran[];
  state : State;
  
  get curSura(): number { return this.state.currentQuranSura; }

  set curSura(value: number) {
    this.state.currentQuranSura = value;
    this.stateService.next(this.state);    
    
  }

  constructor(private repo: Repository, private stateService: StateService) { 

    this.state = this.stateService.getValue();
    this.stateService.pipe(skipWhile(newState => this.checkLocalStateChange(newState)))
               .subscribe(state =>{
                 this.state = state;
                 this.updateSura();
                });

    this.repo.suras.subscribe(data => {
      this.dataLoaded = data.length > 1 ;
    });

  }
 
  checkLocalStateChange(newState: State) : boolean{
    return newState.currentQuranSura != this.curSura   }
  
  updateSura(){
    this.repo.quran.subscribe(q => this.sura = q.filter(q => q.sura == this.curSura) )
  }

  get suras(){
    return this.repo.suras.getValue();
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
