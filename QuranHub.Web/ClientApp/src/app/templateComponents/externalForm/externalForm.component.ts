import { Component, Input, OnInit, OnDestroy } from '@angular/core';

@Component({
  selector: "externalForm",
  templateUrl: "externalForm.component.html"
})

export class ExternalFormComponent implements OnInit, OnDestroy {

  @Input()
  method!: string;

  @Input()
  action!: string;

  @Input()
  buttonLabel!: string;

  @Input()
  target: string = "_self";

  form!:any
  
  constructor() { }

  ngOnInit() {
    this.form = window.document.createElement("form");
    this.form.setAttribute("method", this.method);
    this.form.setAttribute("action", this.action);
    //use _self to redirect in same tab, _blank to open in new tab
    this.form.setAttribute("target", this.target);

    window.document.body.appendChild(this.form);
  }

  ngOnDestroy() {
    window.document.body.removeChild(this.form);
  }

  submit() {
    console.log("submited : " + this.buttonLabel + " , " + this.action);
    this.form.submit();
  }


}
