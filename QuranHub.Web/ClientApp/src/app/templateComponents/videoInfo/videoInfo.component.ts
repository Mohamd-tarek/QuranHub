import { Component, Input } from '@angular/core';
import { VideoInfo } from 'src/app/models/video/VideoInfo.model';

@Component({
  selector: "videoInfo",
  templateUrl: "videoInfo.component.html"
})

export class VideoInfoComponent  {

  @Input()
  info!: VideoInfo;
}
