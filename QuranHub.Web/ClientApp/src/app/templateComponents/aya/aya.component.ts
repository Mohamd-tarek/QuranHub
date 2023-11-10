import { Component, Input, OnChanges, OnInit } from '@angular/core';

@Component({
  selector: "aya",
  templateUrl: "aya.component.html"
})

export class AyaComponent implements OnChanges, OnInit {

  @Input()
  text!: string;

  @Input()
  highLightText!: string;

  wrap:boolean = false;

  ngOnChanges(){
    this.wrap = this.text.length > 250;
  }

  ngOnInit() {
    if (this.highLightText != null) {
     this.text =  this.text.replace(this.highLightText, `<span class='fw-bold bg-warning'>${this.highLightText}</span>`);
    }
  }
}
