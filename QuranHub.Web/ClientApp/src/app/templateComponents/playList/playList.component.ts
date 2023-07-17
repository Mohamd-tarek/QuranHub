import { Component, Input } from '@angular/core';
import { VideoInfo } from 'src/app/models/video/VideoInfo.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: "playList",
  templateUrl: "playList.component.html"
})

export class PlayListComponent  {

  playListName!:string
  infos!: VideoInfo[];
  currentPage : number = 1;
  itemsPerPage: number = 36;
  numOfLinks: number = 0 ; 
  dataLoaded: boolean = false;

  @Input()
  repository:any

  constructor(private activeRoute: ActivatedRoute) 
  {
    this.playListName = this.activeRoute.snapshot.params["name"];
    this.getData();
  }

  navigateEvent(value:number){
     
    this.getData();
  }

  getData(): void {
    this.dataLoaded = false;
    let offset = (this.currentPage  - 1) * this.itemsPerPage ;
    this.repository.getVideoInfoForPlayList(this.playListName, offset, this.itemsPerPage).subscribe((videosInfo:any) => {
       this.infos = videosInfo;
       this.dataLoaded = true;
    })
  }
}
 
