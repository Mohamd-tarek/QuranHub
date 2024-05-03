import { Component, Input } from '@angular/core';
import { PlayListInfo } from 'src/app/models/video/playListInfo.model';
import { Router, ActivatedRoute } from "@angular/router";

@Component({
  selector: "play-list-lnfo",
  templateUrl: "play-list-lnfo.component.html"
})

export class PlayListInfoComponent  {

  @Input()
  info!: PlayListInfo;

  constructor(private router: Router, private route: ActivatedRoute) {

  }

  navigateToPlayList() {
    this.router.navigate(['./playList', this.info.name], { relativeTo: this.route });
  }

}
