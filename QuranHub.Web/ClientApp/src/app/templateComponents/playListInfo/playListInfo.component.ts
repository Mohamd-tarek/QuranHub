import { Component, Input } from '@angular/core';
import { PlayListInfo } from 'src/app/models/video/playListInfo.model';

@Component({
  selector: "playListInfo",
  templateUrl: "playListInfo.component.html"
})

export class PlayListInfoComponent  {

  @Input()
  info!: PlayListInfo;
}
