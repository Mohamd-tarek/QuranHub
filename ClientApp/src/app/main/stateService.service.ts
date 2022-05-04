import { Repository } from "../models/repository";
import { State } from "../models/state";
import { Injectable } from "@angular/core";

@Injectable()
export class StateSevice{
 
  state : State =  new State();
  constructor(private repo : Repository){
       repo.getSessionData<State>("state").subscribe(data =>
        {
            if(data != null)
            {
              this.state = data;
            }
        } );
  }

  get currentSura() : number {
    return this.state.currentSura;
  }
  set currentSura(selection : number)  {
     this.state.currentSura = selection;
     this.update();
  }
  get currentSearch() : string {
    return this.state.currentSearch;
  }

  set currentSearch(value : string) {
     this.state.currentSearch = value;
     this.update();
  }

  get searchForWord() : boolean {
    return this.state.searchForWord;
  }

  set searchForWord(value : boolean)  {
     this.state.searchForWord = value;
     this.update();
  }
  get currentStatisticsPage() : number {
    return this.state.currentStatisticsPage;
  }

  set currentStatisticsPage(value : number)  {
     this.state.currentStatisticsPage = value;
     this.update();
  }

  update(){
    this.repo.storeSessionData("state", this.state);
  }

}
