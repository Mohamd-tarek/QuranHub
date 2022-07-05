import { Component } from '@angular/core';
import { Repository } from "../../../models/repository";
import { Quran } from "../../../models/quran/quran";
import { StateService } from '../../../stateService.service';
import { skipWhile } from 'rxjs/operators';

@Component({
  selector: "quran",
  templateUrl: "quran.component.html"
})
export class QuranComponent {
  
  dataLoaded : boolean = false;
  sura!: Quran[];
  currentQuranSura : number = 1;
  
  get curSura(): number { return this.currentQuranSura; }

  set curSura(value: number) {
    this.currentQuranSura =  value;
    let state: any  = {"currentQuranSura": this.currentQuranSura };
    this.stateService.next(state);    
  }

  constructor(private repo: Repository, private stateService: StateService) { 

    this.stateService.pipe(skipWhile(newState => newState["currentQuranSura"] == this.curSura))
               .subscribe(newState =>{
                 this.currentQuranSura = newState["currentQuranSura"];
                 this.updateSura();
                });

    this.repo.quran.subscribe(data => {
      this.dataLoaded = data.length > 1 ;
    });

  }
 
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
