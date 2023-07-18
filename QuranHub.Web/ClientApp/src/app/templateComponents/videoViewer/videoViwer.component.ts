import { Component, Input, OnInit } from '@angular/core';
import { VideoInfo } from 'src/app/models/video/VideoInfo.model';

@Component({
  selector: "videoViewer",
  templateUrl: "videoViewer.component.html"
})

export class VideoViewerComponent implements OnInit {

  @Input()
  info!: VideoInfo;
  srcValue!: string;

  ngOnInit() {
     
  }
}
