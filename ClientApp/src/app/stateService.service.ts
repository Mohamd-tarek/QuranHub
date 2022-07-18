import { Repository } from "./models/repository";
import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";

@Injectable()

export class StateService extends BehaviorSubject<any>  {
   constructor(private repo : Repository){

    super({
      "authenticated" : false,
      "overviewMode" : false,
      "currentQuranSura" :1,
      "currentTafseerAndTranSura":  1,
      "currentTafseerAndTranAya": 1,
      "currentNoteSura" : 1, 
      "currentNoteAya": 1,
      "currentSearch": "",
      "searchForWord" : false,
      "showLetters" : false,
      "currentStatisticsPage": 1,
      "currentMindMapSura" : 1
     });
    
     repo.getSessionData<any>("state").subscribe(data =>
      {
          if(data != null)
          { 
            let state : any = this.editState(data);
            super.next(state);  
          }
      });
   }

  editState(state: any) : any{    
    let curState: any = this.getValue(); 
    for (let key  of Object.keys(state)){
      curState[key] =  state[key];
    }

   return curState;     
   }

  next(state : any){
      let curState: any = this.editState(state);
       this.update(curState);
       super.next(curState);
 
   }

  update(state : any){ 
    this.repo.storeSessionData("state", state);
  }
}