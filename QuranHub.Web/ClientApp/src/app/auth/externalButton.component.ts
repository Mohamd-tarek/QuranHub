import {Component, Input, OnInit } from "@angular/core";

@Component({
    selector: 'externalButton',
  templateUrl: "externalButton.component.html"
})

export class ExternalButtonComponent implements OnInit {

  @Input()
  provider!: string;

  @Input()
  path!: string;

  targetUrl!: string;

  ngOnInit() {
    this.targetUrl = this.path +'/' +  this.provider;
  } 
}
