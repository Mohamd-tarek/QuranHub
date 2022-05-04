import { Component, Input, Output, EventEmitter } from '@angular/core';
import { StateSevice } from '../../stateService.service';

@Component({
  selector: "pagination",
  templateUrl: "pagination.component.html"
})
export class PaginationComponent {
    start : number= 1;
    elementsPerPage : number = 11;
    curPage  : number;
    startElement: number = this.start;
    endElement: number = this.start + 10;
  
    @Input() total! : number;
    @Input() numOfLinks! : number;
    @Output() selectPageEvent = new EventEmitter<number>();
    
    constructor(private state : StateSevice){
      this.curPage = this.state.currentStatisticsPage;
    }
  
    selectPage (event: any) {
      
      event.preventDefault();
      let nxt =  Number(event.target.getAttribute('value'));

      if(nxt > this.endElement)
        {
        
          this.curPage = nxt;
          this.startElement = nxt;
          this.endElement = Math.min(this.numOfLinks + 1 , nxt + 10) ;
        }
        else if(nxt < this.startElement)
        {
          
            this.curPage = nxt;
            this.startElement = nxt - 10;
            this.endElement = nxt;
        }
        else
        {
          this.curPage  = nxt;
        }
      this.selectPageEvent.emit(nxt);
      
    }

    getLinks() :number[]
    {
      let links :number[] = [];
      for(let i = this.startElement ; i <= this.endElement; ++i)
      {
        
        links.push(i);
      }
      return links;

    }
     
  }     
 

