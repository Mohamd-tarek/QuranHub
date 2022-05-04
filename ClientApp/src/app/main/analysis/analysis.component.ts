import { Component } from '@angular/core';
import { Repository } from "../../models/repository";

@Component({
  selector: "analysis",
  templateUrl: "analysis.component.html"
})
export class AnalysisComponent {


  constructor(private repo: Repository) {
  }
 
}
