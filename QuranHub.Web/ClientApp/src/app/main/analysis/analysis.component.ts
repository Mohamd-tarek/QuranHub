import { Component } from '@angular/core';

@Component({
  selector: "analysis",
  templateUrl: "analysis.component.html"
})

export class AnalysisComponent {

  links :any[] = [{name :"similar", url : "./similar"},
                  {name :"uniques", url : "./uniques"},
                  {name :"topics", url : "./topics"},
                  {name :"compare", url : "./compare"},
                  {name :"lexical analysis", url : "./lexicalAnalysis"},
                  {name :"syntax analysis", url : "./syntaxAnalysis"},
  ]

   constructor() {}
}
