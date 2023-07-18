import { Component } from '@angular/core';
import { DocumentaryRepository } from 'src/app/abstractions/repositories/documentaryRepository';
import { PlayListInfo } from '../../models/video/playListInfo.model';
import { ActivatedRoute } from '@angular/router';



@Component({
  selector: "documentaryPlayList",
  templateUrl: "documentaryPlayList.component.html"
})

export class DocumentaryPlayListComponent  {

  playListInfo!: PlayListInfo;
  dataLoaded: boolean = false;

  constructor(public documentaryRepository: DocumentaryRepository, private activeRoute: ActivatedRoute) {
    let playListName = this.activeRoute.snapshot.params["name"];

    this.documentaryRepository.getPlayListInfo(playListName).subscribe(infos => {
      this.playListInfo = infos;
      this.dataLoaded = true;
    })
  }

}
