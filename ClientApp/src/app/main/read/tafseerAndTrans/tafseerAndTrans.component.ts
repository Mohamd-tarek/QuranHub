import { Component } from '@angular/core';
import { Sura } from '../../../models/meta/sura';
import { Repository } from "../../../models/repository";
import { StateService } from '../../../stateService.service';
import { skipWhile } from 'rxjs/operators';

@Component({
  selector: "tafseerAndTrans",
  templateUrl: "tafseerAndTrans.component.html"
})
export class TafseerAndTransComponent {
 
  currentTafseerAndTranSura :number = 0;
  currentTafseerAndTranAya :number = 0; 
  dataLoaded :boolean = false;
   constructor(private repo: Repository, private stateService : StateService ) {
  
    stateService.pipe(skipWhile(newState  => this.checkLocalStateChange(newState)))
      .subscribe(newState => {
        this.currentTafseerAndTranSura = newState["currentTafseerAndTranSura"];
        this.currentTafseerAndTranAya = newState["currentTafseerAndTranAya"];
        });
  
    this.repo.suras.subscribe(data => {
      this.dataLoaded = data.length > 1 ;
    });
  }

  checkLocalStateChange(newState: any) : boolean{
    return ( newState["currentTafseerAndTranAya"]  == this.curAya &&
             newState["currentTafseerAndTranSura"] == this.curSura);  }
   

  get curSura(): number {
   return this.currentTafseerAndTranSura;
  }

  set curSura(value : number) {
     this.currentTafseerAndTranSura = value;
     let state: any  = {"currentTafseerAndTranSura" : this.currentTafseerAndTranSura };
     this.stateService.next(state);
   }

   get curAya(): number {
    return this.currentTafseerAndTranAya;
   }
 
   set curAya(value : number) {
      this.currentTafseerAndTranAya = value;
      let state: any  = {"currentTafseerAndTranAya": this.currentTafseerAndTranAya}
      this.stateService.next(state);
      
    }

  get suras(): Sura[] {
    return this.repo.suras.getValue();
  }

  get ayas(): number[] {
    let ayas = [];
    for(let i = 1; i <= this.suras[this.curSura - 1].ayas; ++i){
      ayas.push(i);
    }
    return ayas;
  }

  next(){
      if(this.curAya < this.ayas.length){
            this.curAya++;
      }
      else if(this.curSura < 115){
        this.curSura++;
        this.curAya = 1;
      }
  }

  prev(){
    if(this.curAya > 1 ){
          this.curAya--;
    }
    else if(this.curSura > 1){
      this.curSura--;
      this.curAya = 1;
    }
}
}
