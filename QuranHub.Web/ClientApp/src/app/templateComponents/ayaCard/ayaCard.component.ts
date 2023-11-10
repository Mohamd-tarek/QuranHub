import { Component, Input, Output,EventEmitter } from '@angular/core';
import { Quran } from 'src/app/models/quran/quran.model';
import { FadeOutTrigger } from 'src/app/animations/FadeOut.animation';


@Component({
  selector: "ayaCard",
  templateUrl: "ayaCard.component.html",
  animations: [FadeOutTrigger]
})

export class AyaCardComponent  {

  @Input()
  aya!: Quran ;

  @Input()
  closable: boolean = false;

  @Input()
  backgroundColor: string = "white"

  @Input()
  highLightText!: string;

  @Output() removeAyaEvent = new EventEmitter();

  removeAya(): void{
    this.removeAyaEvent.emit();
  }
}
