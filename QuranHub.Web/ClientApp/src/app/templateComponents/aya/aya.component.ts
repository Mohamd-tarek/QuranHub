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

  @Input()
  disableWrap:boolean = false;

  wrap:boolean = false;

  ngOnChanges(){
    if(this.disableWrap == false)
    this.wrap = this.text.length > 250;
    else
    {
      this.wrap = false
    }
  }

  ngOnInit() {
    if (this.highLightText != null) {
     this.text =  this.text.replace(this.highLightText, `<span class='fw-bold bg-warning'>${this.highLightText}</span>`);
    }
  }
}
