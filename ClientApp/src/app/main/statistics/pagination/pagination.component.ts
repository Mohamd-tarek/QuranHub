import { Component, Input, Output, EventEmitter } from '@angular/core';
import { StateService } from '../../../stateService.service';
import { State } from 'src/app/models/state';

@Component({
  selector: "pagination",
  templateUrl: "pagination.component.html"
})
export class PaginationComponent {
    state : State;
    start : number= 1;
    linksPerPage : number = 10;
    startElement: number = this.start;
    endElement: number = this.start + this.linksPerPage;
  
    @Input() numOfLinks! : number;
    
    constructor(private stateService : StateService){
      this.state = this.stateService.getValue();
    }
  
    selectPage (event: any) {
      
      event.preventDefault();
      let nxt =  Number(event.target.getAttribute('value'));
      nxt  = this.handleEdgeCases(nxt);
      this.updateState(nxt);      
    }

    handleEdgeCases(nxt : number) : number{
      nxt = nxt > this.numOfLinks ? this.numOfLinks + 1 : nxt;
      nxt = nxt < 1 ? 1 : nxt;
      
      if(nxt > this.endElement)
        {    
          this.startElement = nxt;
          this.endElement = Math.min(this.numOfLinks + 1 , nxt + this.linksPerPage) ;
        }
        else if(nxt < this.startElement)
        {
          this.startElement = nxt - this.linksPerPage;
          this.endElement = nxt;
        }
        return nxt;
    }

    updateState(nxt : number){
      this.state.currentStatisticsPage = nxt;
      this.stateService.next(this.state);
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
 

