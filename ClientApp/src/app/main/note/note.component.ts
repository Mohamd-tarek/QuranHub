import { Component } from '@angular/core';
import { State } from 'src/app/models/state';
import { Sura } from '../../models/meta/sura';
import { Repository } from "../../models/repository";
import { StateSevice } from '../stateService.service';

@Component({
  selector: "note",
  templateUrl: "note.component.html"
})
export class NoteComponent {
  state : State;
  
  constructor(private repo: Repository, private stateService : StateSevice ) {
    this.state = this.stateService.getValue();
    this.repo.suras;    
  }

  get curSura(): number {
   return this.state.currentNoteSura;
  }

  set curSura(value : number) {
     this.state.currentNoteSura = value;
     this.stateService.next(this.state);
   }

   get curAya(): number {
    return this.state.currentNoteAya;
   }
 
   set curAya(value : number) {
      this.state.currentNoteAya = value;
      this.stateService.next(this.state);
    }

  get suras(): Sura[] {
    return this.repo.suras;
  }

  get ayas(): number[] {
    let ayas = [];
    for(let i = 1; i <= this.repo.suras[this.curSura - 1].ayas; ++i){
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
