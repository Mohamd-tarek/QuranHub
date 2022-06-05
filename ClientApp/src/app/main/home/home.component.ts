import { Component } from '@angular/core';
import { Repository } from "../../models/repository";

@Component({
  selector: "home",
  templateUrl: "home.component.html"
})
export class HomeComponent {


  constructor(private repo: Repository) {
  }
 
}
