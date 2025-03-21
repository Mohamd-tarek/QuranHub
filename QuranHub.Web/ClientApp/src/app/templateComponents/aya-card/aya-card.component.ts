import { Component, Input, Output,EventEmitter } from '@angular/core';
import { Quran } from 'src/app/models/quran/quran.model';
import { FadeOutTrigger } from 'src/app/animations/FadeOut.animation';
import { QuranRepository } from 'src/app/abstractions/repositories/quranRepository';


@Component({
  selector: "aya-card",
  templateUrl: "aya-card.component.html",
  animations: [FadeOutTrigger]
})

export class AyaCardComponent  {
  
  constructor(private quranRepo: QuranRepository){

  }
  
  english:boolean = false;
  
  @Input()
  aya!: Quran ;

  @Input()
  isHadith: boolean = false ;

  @Input()
  closable: boolean = false;

  @Input()
  backgroundColor: string = "white"

  @Input()
  highLightText!: string;

  @Input()
  disableWrap: boolean = false;

  @Output() removeAyaEvent = new EventEmitter();

  removeAya(): void{
    this.removeAyaEvent.emit();
  }

  transtalte(){
    if(!this.english)
    {
      this.quranRepo.translation.subscribe((data:any) => {
       this.aya = data[this.aya.sura][this.aya.aya - 1];
       this.english = true;
       }
      )
    }
    else
    {
      this.quranRepo.quran.subscribe((data:any) => {
        this.aya = data[this.aya.sura][this.aya.aya - 1];
        this.english = false;
        }
       )
    }

  }
}
