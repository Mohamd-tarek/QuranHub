import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';

@Component({
  selector: "pagination",
  templateUrl: "pagination.component.html"
})
export class PaginationComponent implements OnInit {

  @Input()
  numOfLinks!: number;

  @Input()
  linksPerPage: number = 10;

  @Input()
  current: number = 1;

  startElement: number = 1;
  endElement: number = this.startElement + this.linksPerPage;

  @Output()
  navigateEvent = new EventEmitter<number>();
  
  ngOnInit(){
    this.startElement = this.current - (this.current % this.linksPerPage) + 1;
    this.endElement = this.startElement + this.linksPerPage - 1;
    this.linksPerPage = Math.min(this.linksPerPage, this.numOfLinks);
  }

  selectPage(event: any) {
    event.preventDefault();
    this.current =  Number(event.target.getAttribute('value'));
    this.current  = this.handleEdgeCases(this.current);
    this.navigateEvent.emit(this.current);
  }

  handleEdgeCases(nxt : number) : number {
    //check upper bound
    nxt = nxt > this.numOfLinks ? this.numOfLinks + 1 : nxt;
    //check lower bound
    nxt = nxt < 1 ? 1 : nxt;
    
    //current viewd upper bound
    if(nxt > this.endElement){    
      this.startElement = nxt;
      this.endElement = Math.min(this.numOfLinks  , nxt + this.linksPerPage - 1) ;
    }
     //current viewd lower bound
    else if(nxt < this.startElement){
      this.startElement = nxt - this.linksPerPage + 1;
      this.endElement = nxt;
    }
    return nxt;
  }

  getLinks() :number[] {
    let links :number[] = [];
    for(let i = this.startElement ; i <= this.endElement; ++i){
      links.push(i);
    }
    return links;
  }
    
}     
 

