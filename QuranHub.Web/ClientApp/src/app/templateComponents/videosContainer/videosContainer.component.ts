import { Component, Input } from '@angular/core';
import { VideoInfo } from 'src/app/models/video/VideoInfo.model';

@Component({
  selector: "videosContainer",
  templateUrl: "videosContainer.component.html"
})

export class VideosContainerComponent  {

  @Input()
  infos!: VideoInfo[];

  @Input()
  numOfCol!: number;

  @Input()
  bordered: boolean = false;

  buildTable() : any[]{
    let dataIndex =  0;
    let dataSize = this.infos.length;
    let table = [];
    let row:VideoInfo[] = [];
    let curRow = 0;
    let curCol = 0;

    while(dataIndex < dataSize)
    {
      row.push(this.infos[dataIndex]);
      curCol  = (curCol + 1) % this.numOfCol;

      if(curCol === 0 || dataIndex + 1 === dataSize)
      {
        curRow++;
        table.push(row);
        row = [];
      }

      dataIndex = curRow * this.numOfCol + curCol;
    }
    return table;

  }
}
