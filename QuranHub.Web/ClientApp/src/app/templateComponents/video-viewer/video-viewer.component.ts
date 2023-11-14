import { Component, Input, OnInit } from '@angular/core';
import { VideoInfo } from 'src/app/models/video/VideoInfo.model';

@Component({
  selector: "video-viewer",
  templateUrl: "video-viewer.component.html"
})

export class VideoViewerComponent  {

  @Input()
  info!: VideoInfo;

  @Input()
  textEnd: boolean = false;



 
}
