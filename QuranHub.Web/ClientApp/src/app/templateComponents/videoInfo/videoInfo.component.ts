import { Component, Input } from '@angular/core';
import { VideoInfo } from 'src/app/models/video/VideoInfo.model';
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "videoInfo",
  templateUrl: "videoInfo.component.html"
})

export class VideoInfoComponent  {

  @Input()
  info!: VideoInfo;

  constructor(private router: Router, private route: ActivatedRoute) {

  }

  navigateToVideo() {
    this.router.navigate(['../videoViewer' , this.info.name], { relativeTo: this.route });
  }
}
