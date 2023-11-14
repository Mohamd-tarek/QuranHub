import { Component, Input } from '@angular/core';
import { VideoInfo } from 'src/app/models/video/VideoInfo.model';
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "video-info",
  templateUrl: "video-info.component.html"
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
