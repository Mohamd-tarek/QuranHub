import { Component, Input } from '@angular/core';

@Component({
  selector: "tabledContainer",
  templateUrl: "tabledContainer.component.html"
})

export class TabledContainerComponent {

  @Input()
  numOfCol!: number;

  @Input()
  tableData!: [];

  @Input()
  bordered: boolean = false;

  buildTable() : any[]{
    let dataIndex =  0;
    let dataSize = this.tableData.length;
    let table = [];
    let row:[] = [];
    let curRow = 0;
    let curCol = 0;

    while(dataIndex < dataSize)
    {
      row.push(this.tableData[dataIndex]);
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
