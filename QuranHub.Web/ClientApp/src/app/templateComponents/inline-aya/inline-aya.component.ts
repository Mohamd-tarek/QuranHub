import { AfterViewChecked, Renderer2, AfterViewInit, Component, Input, OnInit, ViewContainerRef } from '@angular/core';
import { Quran } from 'src/app/models/quran/quran.model';
import { BackgroundHighlightTrigger } from 'src/app/animations/backgroundHighlight.animation';


@Component({
  selector: "inline-aya",
  templateUrl: "inline-aya.component.html",
  animations: [BackgroundHighlightTrigger]
})

export class InlineAyaComponent    {

  @Input()
  aya!: Quran;

  @Input()
  focused:boolean = false;

}
