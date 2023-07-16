import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { UserBasicInfo } from "../models/user/userBasicInfo.model";
import { HomeService } from '../abstractions/services/homeService';

@Component({
  selector: 'peopleResults',
  templateUrl: './peopleResults.component.html',
})

export class PeopleResultsComponent  {

  query!: string;
  users!: UserBasicInfo[];
  dataLoaded: boolean = false;

  constructor(
    private activeRoute: ActivatedRoute,
    private homeDataService: HomeService) {

    this.activeRoute.params.subscribe( params => {
      this.query = params["q"];
      this.homeDataService.findUsersByName(this.query).subscribe(result => {
        this.users = result;
        this.dataLoaded = true;
      });
    });
  }
 
}
