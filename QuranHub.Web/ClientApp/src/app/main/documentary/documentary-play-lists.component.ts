import { Component } from '@angular/core';
import { DocumentaryRepository } from '../../abstractions/repositories/documentaryRepository';
import { PlayListInfo } from '../../models/video/playListInfo.model';

@Component({
  selector: "documentary-play-lists",
  templateUrl: "documentary-play-lists.component.html"
})

export class DocumentaryPlayListsComponent  {

  playListsInfo!: PlayListInfo[]

  dataLoaded: boolean = false

  constructor(public documentaryRepository: DocumentaryRepository) {
    this.documentaryRepository.getPlayLists().subscribe(infos => {
      this.playListsInfo = infos;
      this.dataLoaded = true;
    })
  }

}
