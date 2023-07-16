import { Component, Input, Output,EventEmitter } from '@angular/core';
import { Quran } from 'src/app/models/quran/quran.model';


@Component({
  selector: "ayaCard",
  templateUrl: "ayaCard.component.html"
})

export class AyaCardComponent  {

  @Input()
  aya!: Quran ;

  @Input()
  closable: boolean = false;

  @Input()
  backgroundColor: string = "white"

  @Output() removeAyaEvent = new EventEmitter();

  removeAya(): void{
    this.removeAyaEvent.emit();
  }
}
