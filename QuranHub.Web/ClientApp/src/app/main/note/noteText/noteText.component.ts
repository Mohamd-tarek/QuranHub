import { Component, OnDestroy } from '@angular/core';
import { QuranRepository } from "../../../abstractions/repositories/quranRepository";
import { Note } from 'src/app/models/quran/note.model';
import { Quran } from 'src/app/models/quran/quran.model';
import { StateService } from '../../../abstractions/services/stateService';
import { skipWhile } from 'rxjs/operators';
import {Subscription } from "rxjs";

@Component({
  selector: "noteText",
  templateUrl: "noteText.component.html"
})

export class NoteTextComponent implements OnDestroy  {

  subscription: Subscription;
  currentNoteSura: number = 0;
  currentNoteAya: number = 0;
  aya! : Quran;
  note! : Note;
  edit : boolean = false;
  dataLoaded: boolean = false; 

  constructor(private repo: QuranRepository, private stateService : StateService) {
  this.subscription = stateService.pipe(skipWhile((newState:any) =>   this.checkLocalStateChange(newState)))
    .subscribe((newState:any) => {
         this.setState(newState);
      });
   
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  checkLocalStateChange(newState:any) : boolean{
    return ( newState["currentNoteSura"]  == this.currentNoteSura &&
             newState["currentNoteAya"] == this.currentNoteAya);
  }
  
             
  setState(newState: any) : void{
    this.currentNoteSura = newState["currentNoteSura"];
    this.currentNoteAya = newState["currentNoteAya"];  
    this.getAya();  
  }
  
  getAya():void{
    this.repo.quran.subscribe((data:any) => { 
           if(data.length > 1){
             this.aya = 
             data[this.currentNoteSura][this.currentNoteAya - 1];
             this.getNote();
           }
        });
  }

  getNote() : void{
    this.repo.getNote(this.aya).subscribe(
      resp => {
             this.note = resp != null ? resp 
                         : new Note(0, this.aya.index, this.aya.sura, this.aya.aya, "no notes");
       });
  }

  saveNote(){
    this.repo.insertNote(this.note);
    this.edit = false;
  }
  
}
