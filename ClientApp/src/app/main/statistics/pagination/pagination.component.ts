import { Component, Input, Output, EventEmitter } from '@angular/core';
import { StateSevice } from '../../stateService.service';
import { State } from 'src/app/models/state';

@Component({
  selector: "pagination",
  templateUrl: "pagination.component.html"
})
export class PaginationComponent {
    state : State;
    start : number= 1;
    elementsPerPage : number = 11;
    startElement: number = this.start;
    endElement: number = this.start + 10;
  
    @Input() total! : number;
    @Input() numOfLinks! : number;
    @Output() selectPageEvent = new EventEmitter<number>();
    
    constructor(private stateService : StateSevice){
      this.state = this.stateService.getValue();
    }
  
    selectPage (event: any) {
      
      event.preventDefault();
      let nxt =  Number(event.target.getAttribute('value'));

      if(nxt > this.endElement)
        {
        
          this.state.currentStatisticsPage = nxt;
          this.startElement = nxt;
          this.endElement = Math.min(this.numOfLinks + 1 , nxt + 10) ;
        }
        else if(nxt < this.startElement)
        {
          
            this.state.currentStatisticsPage = nxt;
            this.startElement = nxt - 10;
            this.endElement = nxt;
        }
        else
        {
          this.state.currentStatisticsPage  = nxt;
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
 

