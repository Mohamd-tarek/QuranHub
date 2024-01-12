import { StateService } from "../abstractions/services/stateService";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { restApiPaths } from "../constants/state.constants";

@Injectable()

export class StateDataService extends StateService  {
  constructor(private http: HttpClient){

    super({
      "overviewMode" : false,
      "currentLanguage": "arabic",
      "currentQuranSura" :1,
      "currentTafseerAndTranSura":  1, 
      "currentTafseerAndTranAya": 1,
      "currentNoteSura" : 1, 
      "currentNoteAya": 1,
      "currentSearch": "",
      "currentTafseerSura": 1,
      "currentTafseerAya": 1,
      "currentTafseerTafseer": "muyassar",
      "searchForWord" : false,
      "showLetters" : false,
      "currentStatisticsPage": 1,
      "currentMindMapSura" : 1,
      "currentSimilarSura":  1,
      "currentSimilarAya": 1,
      "currentLexicalSura":  1,
      "currentLexicalAya": 1,
      "currentSyntaxSura":  1,
      "currentSyntaxAya": 1,
      "compareSetInfo": []

     });
    
    this.getSessionData<any>("state").subscribe((data:any) => {
      if(data != null) { 
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
    this.storeSessionData("state", state);
  }

  storeSessionData<T>(dataType: string, data: T) {
    return this.http.post(restApiPaths.SessionURL + dataType, data).subscribe(response => { });
  }

  getSessionData<T>(dataType: string): Observable<T> {
    return this.http.get<T>(restApiPaths.SessionURL + dataType);
  }
}
