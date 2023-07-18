import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DocumentaryRepository } from 'src/app/abstractions/repositories/documentaryRepository';
import { VideoInfo } from 'src/app/models/video/VideoInfo.model';



@Component({
  selector: "documentaryVideoViewer",
  templateUrl: "documentaryVideoViewer.component.html"
})

export class DocumentaryVideoViewerComponent  {

  info!: VideoInfo;
  dataLoaded: boolean = false;

  constructor(public documentaryRepository: DocumentaryRepository, private activeRoute: ActivatedRoute) {
    let videoName = this.activeRoute.snapshot.params["name"];

    this.documentaryRepository.GetVideoInfoAsync(videoName).subscribe(info => {
      this.info = info;
      this.dataLoaded = true;
    })
  }

}
