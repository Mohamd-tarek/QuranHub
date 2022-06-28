import { Component } from '@angular/core';
import { Sura } from '../../models/meta/sura';
import { Repository } from "../../models/repository";
import { StateService } from '../../stateService.service';
import { skipWhile } from 'rxjs/operators';

@Component({
  selector: "note",
  templateUrl: "note.component.html"
})
export class NoteComponent {
  currentNoteSura: number = 1;
  currentNoteAya: number = 1;
  
  constructor(private repo: Repository, private stateService : StateService ) {
    stateService.pipe(skipWhile(newState => this.checkLocalStateChange(newState)))
    .subscribe(newState => {
         this.currentNoteSura = newState["currentNoteSura"];
         this.currentNoteAya = newState["currentNoteAya"];
      });
  }

  checkLocalStateChange(newState: any) : boolean{
    return ( newState["currentNoteSura"]  == this.currentNoteSura &&
             newState["currentNoteAya"] == this.currentNoteAya);  }
   

  get curSura(): number {
   return this.currentNoteSura;
  }

  set curSura(value : number) {
     this.currentNoteSura = value;
     let state: any  = {"currentNoteSura": this.currentNoteSura}
     this.stateService.next(state);
   }

   get curAya(): number {
    return this.currentNoteAya;
   }
 
   set curAya(value : number) {
      this.currentNoteAya = value;
      let state: any = {"currentNoteAya": this.currentNoteAya}
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
