import { Component, OnDestroy } from '@angular/core';
import { StateService } from '../../abstractions/services/stateService';
import { FormControl } from '@angular/forms';
import { skipWhile } from 'rxjs/operators';
import {Subscription } from "rxjs";

@Component({
  selector: "read",
  templateUrl: "read.component.html"
})

export class ReadComponent implements OnDestroy {

  subscription: Subscription;
  overviewMode : FormControl = new FormControl(false); 
  seperateVerses : FormControl = new FormControl(false);  
  disableWrap: FormControl = new FormControl(false); 
  languages: string [] = ["arabic", "english"] ; 
  currentLanguage:string = this.languages[0];

  constructor(private stateService : StateService){
    this.subscription = this.stateService.pipe(skipWhile((newState:any)  => this.checkLocalStateChange(newState)))
    .subscribe((newState:any) => {
         this.setState(newState)
      });

    this.overviewMode.valueChanges.subscribe(()=>{
      let state: any = {"overviewMode": this.overviewMode.value}
      this.stateService.next(state);
    })
    this.seperateVerses.valueChanges.subscribe(()=>{
      let state: any = {"seperateVerses": this.seperateVerses.value}
      this.stateService.next(state);
    })
    this.disableWrap.valueChanges.subscribe(()=>{
      let state: any = {"disableWrap": this.disableWrap.value}
      this.stateService.next(state);
    })
  }

  checkLocalStateChange(newState: any) : boolean{
    return ( newState["overviewMode"] == this.overviewMode.value &&
             newState["seperateVerses"] == this.seperateVerses.value &&
             newState["disableWrap"] == this.overviewMode.value &&
             newState["currentLanguage"] == this.currentLanguage);  }

  setState(newState: any):void{
    this.overviewMode.setValue(newState["overviewMode"], {emitEvent :false});
    this.seperateVerses.setValue(newState["seperateVerses"], {emitEvent :false});
    this.disableWrap.setValue(newState["disableWrap"], {emitEvent :false});
    this.currentLanguage = newState["currentLanguage"];
  }

  get curLanguage(){
    return this.currentLanguage;
  }

  set curLanguage(value){
    this.currentLanguage = value;
    let state: any  = {"currentLanguage" : this.currentLanguage }
    this.stateService.next(state);
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
   
}
