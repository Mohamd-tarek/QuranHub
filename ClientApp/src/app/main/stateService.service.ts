import { Repository } from "../models/repository";
import { State } from "../models/state";
import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable()

export class StateSevice extends BehaviorSubject<State>  {
   constructor(private repo : Repository){
    super( new State());
    
     repo.getSessionData<State>("state").subscribe(data =>
      {
          if(data != null)
          {
            super.next(data);
            
          }
      } );
   }

   next(state : State){
       this.update(state);
       super.next(state);

   }

   update(state : State){
    this.repo.storeSessionData("state", state);
  }
}