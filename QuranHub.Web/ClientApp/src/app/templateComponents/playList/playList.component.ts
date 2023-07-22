import { Component, Input, OnInit } from '@angular/core';
import { VideoInfo } from 'src/app/models/video/VideoInfo.model';
import { PlayListInfo } from '../../models/video/playListInfo.model';


@Component({
  selector: "playList",
  templateUrl: "playList.component.html"
})

export class PlayListComponent implements OnInit {


  infos!: VideoInfo[];
  currentPage : number = 1;
  itemsPerPage: number = 24;
  numOfLinks: number = 0;
  linksPerPage: number = 10;
  dataLoaded: boolean = false;

  @Input()
  repository: any

  @Input()
  playListInfo!: PlayListInfo;

  constructor() { }

  ngOnInit() {
    this.numOfLinks = Math.trunc(this.playListInfo.numberOfVideos / this.itemsPerPage);
    this.linksPerPage = Math.min(this.linksPerPage, this.numOfLinks);
    this.getData();
    
  }

  navigateEvent(value: number) {
    console.log("navigated : "  + value);
    this.currentPage = value;
    this.getData();
  }

  getData(): void {
    this.dataLoaded = false;
    let offset = (this.currentPage - 1) * this.itemsPerPage;
    this.repository.getVideoInfoForPlayList(this.playListInfo.name, offset, this.itemsPerPage).subscribe((videosInfo: any) => {
       this.infos = videosInfo;
       this.dataLoaded = true;
    })
  }
}
 
