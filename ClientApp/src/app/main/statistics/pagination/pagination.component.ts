import { Component, Input } from '@angular/core';
import { StateService } from '../../../stateService.service';
import { skipWhile } from 'rxjs/operators';


@Component({
  selector: "pagination",
  templateUrl: "pagination.component.html"
})
export class PaginationComponent {
    currentStatisticsPage: number = 1;
    start : number= 1;
    linksPerPage : number = 10;
    startElement: number = this.start;
    endElement: number = this.start + this.linksPerPage;
  
    @Input() numOfLinks! : number;
    
    constructor(private stateService : StateService){
      stateService.pipe(skipWhile(newState => this.currentStatisticsPage == newState["currentStatisticsPage"]))
    .subscribe(newState => {
         this.currentStatisticsPage = newState["currentStatisticsPage"];
      });
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
      this.currentStatisticsPage = nxt;
      let state: any = {"currentStatisticsPage": this.currentStatisticsPage}
      this.stateService.next(state);
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
 

