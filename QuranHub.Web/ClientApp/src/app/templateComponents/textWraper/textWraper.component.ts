import { Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';

@Component({
  selector: "textWraper",
  templateUrl: "textWraper.component.html"
})
export class TextWraperComponent implements OnInit, OnChanges {

  @Input()
  text!: string;

  @Input()
  length!: number;

  @Input()
  backgroundColor:string = "";

  textToView: string = "";
  seeMore:boolean = true;
  
  constructor() {}

  ngOnInit(): void {
     this.textToView = this.text.substring(0, this.length);
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.textToView = this.text.substring(0, this.length);
    this.seeMore = true;
  }

  seeAll(){
    this.textToView = this.text;
    this.seeMore = false;
  }
}
