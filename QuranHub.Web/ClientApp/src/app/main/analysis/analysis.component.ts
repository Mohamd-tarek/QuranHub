import { Component } from '@angular/core';

@Component({
  selector: "analysis",
  templateUrl: "analysis.component.html"
})

export class AnalysisComponent {

  links :any[] = [{name :"SIMIALR", url : "./similar"},
                  {name :"UNIQUES", url : "./uniques"},
                  {name :"TOPICS", url : "./topics"},
                  {name :"COMPARE", url : "./compare"},
                  {name :"LEXICAL_ANALUYSIS", url : "./lexicalAnalysis"},
                  {name :"SYNTAX_ANALYSIS", url : "./syntaxAnalysis"},
  ]

   constructor() {}
}
