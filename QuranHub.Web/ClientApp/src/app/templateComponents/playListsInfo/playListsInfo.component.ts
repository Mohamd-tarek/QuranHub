import { Component, Input } from '@angular/core';
import { PlayListInfo } from 'src/app/models/video/playListInfo.model';

@Component({
  selector: "playListsInfo",
  templateUrl: "playListsInfo.component.html"
})

export class PlayListsInfoComponent  {

  @Input()
  infos!: PlayListInfo[];

  @Input()
  numOfCol!: number;

  @Input()
  bordered: boolean = false;
 

  buildTable() : any[]{
    let dataIndex =  0;
    let dataSize = this.infos.length;
    let table = [];
    let row:PlayListInfo[] = [];
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
