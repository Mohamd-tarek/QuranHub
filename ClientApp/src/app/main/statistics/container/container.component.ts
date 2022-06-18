import { Component, Input } from '@angular/core';

@Component({
  selector: "container",
  templateUrl: "container.component.html"
})
export class ContainerComponent {
  @Input() col! : number;
  @Input() data! : [];

  getElements() : any[]{
        let content = [];
        let size = this.data.length;
        let curRow = 0;
        let curCol = 0;
        let index =  0;
        let current = [];

        while(index < size)
        {
          current.push([this.data[index][0], this.data[index][1]]);
          curCol  = (curCol + 1) % this.col;

          if(curCol === 0 || index + 1 === size)
          {
                  curRow++;
                  content.push(current);
                  current = [];
          }

          index = curRow * this.col + curCol;
        }
        return content;

  }

  
}
