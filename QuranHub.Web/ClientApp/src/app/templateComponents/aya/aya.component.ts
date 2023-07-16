import { Component, Input, OnChanges, OnInit } from '@angular/core';

@Component({
  selector: "aya",
  templateUrl: "aya.component.html"
})

export class AyaComponent implements OnChanges {

  @Input()
  text!: string;

  wrap:boolean = false;

  ngOnChanges(){
    this.wrap = this.text.length > 250;
  }
}
