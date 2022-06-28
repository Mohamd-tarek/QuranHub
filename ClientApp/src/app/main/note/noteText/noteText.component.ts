import { Component } from '@angular/core';
import { Repository } from "../../../models/repository";
import { Note } from 'src/app/models/quran/note';
import { Quran } from 'src/app/models/quran/quran';
import { StateService } from '../../../stateService.service';
import { skipWhile } from 'rxjs/operators';


@Component({
  selector: "noteText",
  templateUrl: "noteText.component.html"
})
export class NoteTextComponent {
  currentNoteSura: number = 1;
  currentNoteAya: number = 1;
  aya! : Quran;
  note! : Note;
  edit : boolean = false;
  dataLoaded: boolean = false; 


  constructor(private repo: Repository, private stateService : StateService) {
    stateService.pipe(skipWhile(newState =>   this.checkLocalStateChange(newState)))
    .subscribe(newState => {
         this.updateState(newState);
         this.getNote();
      });
   
  }

  updateState(newState: any) : void{
    this.currentNoteSura = newState["currentNoteSura"];
    this.currentNoteAya = newState["currentNoteAya"];  
    this.getAya();  
  }
  
  getAya():void{
    this.repo.quran.subscribe(data => this.aya = 
           data.filter(q => q.sura === this.currentNoteSura)[this.currentNoteAya - 1]);
  }

  getNote() : void{
    this.repo.getNote(this.aya).subscribe(
      resp => {
             this.note = resp != null ? resp 
                         : new Note(0, this.aya.index, this.aya.sura, this.aya.aya, "no notes");
       });
  }

  checkLocalStateChange(newState:any) : boolean{
    return ( newState["currentNoteSura"]  == this.currentNoteSura &&
             newState["currentNoteAya"] == this.currentNoteAya);  }
   

  saveNote(){
    this.repo.insertNote(this.note);
    this.edit = false;
  }
  
}
