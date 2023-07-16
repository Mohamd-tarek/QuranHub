import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Quran } from 'src/app/models/quran/quran.model';

@Component({
  selector: "ayaSetContainer",
  templateUrl: "ayaSetContainer.component.html"
})

export class AyaSetContainerComponent  {

  @Input()
  ayaSet: Quran[] = [];

  @Output() removeAyaEvent = new EventEmitter<Quran>();

  removeAya(index: number): void{
    this.removeAyaEvent.emit(this.ayaSet[index]);
    this.ayaSet.splice(index, 1);
  }

}
