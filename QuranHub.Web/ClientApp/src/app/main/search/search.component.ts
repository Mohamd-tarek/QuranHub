import { Component, OnDestroy } from '@angular/core';
import { QuranRepository } from "../../abstractions/repositories/quranRepository";
import { FormControl } from '@angular/forms';
import { Sura } from 'src/app/models/meta/sura.model';
import { StateService } from '../../abstractions/services/stateService';
import { skipWhile } from 'rxjs/operators';
import {Subscription } from "rxjs";

@Component({
  selector: "search",
  templateUrl: "search.component.html"
})

export class SearchComponent implements OnDestroy {

  subscription: Subscription;
  result : any[] = []; 
  searchForWord: FormControl = new FormControl();
  currentSearch: FormControl = new FormControl();
  dataLoaded : boolean = false;

  get suras() : Sura[]{
    return this.repo.suras.getValue();
  }

  constructor(private repo: QuranRepository, private stateService : StateService) {
       
    this.repo.quranClean.subscribe((data:any) => this.dataLoaded = data.length > 1);
    this.subscription = this.stateService.pipe(skipWhile((newState:any)  => this.checkLocalStateChange(newState)))
    .subscribe((newState:any) => {
        this.setState(newState);
      });

    this.searchForWord.valueChanges.subscribe(()=> { 
      let state: any = {"searchForWord": this.searchForWord.value}
      this.stateService.next(state);
      this.setResult(this.currentSearch.value);
      }); 

    this.currentSearch.valueChanges.subscribe(()=> { 
      let state: any  = {"currentSearch": this.currentSearch.value}
      this.stateService.next(state);
      this.setResult(this.currentSearch.value);
      });    
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  checkLocalStateChange(newState: any) : boolean{
    return ( newState["currentSearch"] == this.currentSearch.value &&
             newState["searchForWord"] == this.searchForWord.value); 
  }
  
  setState(newState: any) : void{
    this.currentSearch.setValue(newState["currentSearch"], {emitEvent :false});
    this.searchForWord.setValue(newState["searchForWord"], {emitEvent :false});
    this.setResult(this.currentSearch.value);
  }
   

  setResult(word :string){ 
    if(this.searchForWord.value === true){
      this.repo.trie.subscribe((data:any) => this.result = word.length > 1 ? data.find(word) : [])
    }
    else{
      if(word.length > 1){
        this.repo.quranClean.subscribe((data:any) =>this.result = data.filter((q:any) => q.text.includes(word)));                                              
      }
      else
      {
        this.result = [];
      }
    }
  }

  removeItem(index: number): void{
    this.result.splice(index, 1);
  }
}
