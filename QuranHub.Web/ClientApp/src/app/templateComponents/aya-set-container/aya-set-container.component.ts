import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Quran } from 'src/app/models/quran/quran.model';

@Component({
  selector: "aya-set-container",
  templateUrl: "aya-set-container.component.html"
})

export class AyaSetContainerComponent  {

  @Input()
  ayaSet: Quran[] = [];

  @Input()
  isHadith: boolean = false;

  @Input()
  highLightText!: string;

  @Output() removeAyaEvent = new EventEmitter<Quran>();

  removeAya(index: number): void{
    this.removeAyaEvent.emit(this.ayaSet[index]);
    this.ayaSet.splice(index, 1);
  }

}
