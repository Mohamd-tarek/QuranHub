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
  writingComment: boolean = false;
  commentAdded: boolean = false;
  showLikes: boolean = false;
  show: boolean = true;
  editinfo: boolean = false;

  constructor(public documentaryRepository: DocumentaryRepository, private activeRoute: ActivatedRoute) {
    let videoName = this.activeRoute.snapshot.params["name"];

    this.documentaryRepository.GetVideoInfoAsync(videoName).subscribe(info => {
      this.info = info;
      console.log(this.info);
      this.dataLoaded = true;
    })
  }

  likeEvent() {
    this.info.reactedTo = true;
    this.documentaryRepository.addReact(1, this.info.videoInfoId).subscribe(like => {
      this.info.reactedTo = true;
      this.info.reactsCount++;
    },
      error => {
        this.info.reactedTo = false;
      });

  }

  unlikeEvent() {
    this.info.reactedTo = false;
    this.documentaryRepository.removeReact(this.info.videoInfoId).subscribe(response => {
      this.info.reactedTo = false;
      this.info.reactsCount--;
    },
      error => {
        this.info.reactedTo = true;
      });

  }

  showLikesEvent() {
    this.showLikes = true;
  }

  hideLikesEvent() {
    this.showLikes = false;
  }




  startWritingComment() {
    this.writingComment = true;
  }

  cancelWritingComment() {
    this.writingComment = false;
  }

  

}
