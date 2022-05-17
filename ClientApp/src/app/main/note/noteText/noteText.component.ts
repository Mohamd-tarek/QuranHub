import { Component } from '@angular/core';
import { Repository } from "../../../models/repository";
import { Note } from 'src/app/models/quran/note';
import { Quran } from 'src/app/models/quran/quran';
import { State } from 'src/app/models/state';
import { StateSevice } from '../../stateService.service';
import { skipWhile } from 'rxjs/operators';


@Component({
  selector: "noteText",
  templateUrl: "noteText.component.html"
})
export class NoteTextComponent {
  state : State;
  aya : Quran;
  note! : Note;
  edit : boolean = false;


  constructor(private repo: Repository, private stateService : StateSevice) {
    this.state = this.stateService.getValue();
    this.aya = this.repo.quran.filter(q => q.sura == this.state.currentNoteSura)[this.state.currentNoteAya - 1];
    this.note = new Note(0, this.aya.index, this.aya.sura, this.aya.aya, "no notes");

    this.stateService.pipe(skipWhile(newState => newState.currentNoteAya != this.state.currentNoteAya))
               .subscribe(newState =>{
                 this.state = newState;
                 this.aya = this.repo.quran.filter(q => q.sura == this.state.currentNoteSura && q.aya == this.state.currentNoteAya)[0];
                 this.repo.getNote(this.aya).subscribe(
                             resp => { if(resp != null){ this.note = resp }},
                             err => {console.log("no data" + err);
                            });
                                      });

  }

saveNote(){
  this.repo.insertNote(this.note);
  this.edit = false;
}
  
}
