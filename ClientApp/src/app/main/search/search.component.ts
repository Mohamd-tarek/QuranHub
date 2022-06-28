import { Component } from '@angular/core';
import { Repository } from "../../models/repository";
import { FormControl } from '@angular/forms';
import { Sura } from 'src/app/models/meta/sura';
import { StateService } from '../../stateService.service';
import { skipWhile } from 'rxjs/operators';


@Component({
  selector: "search",
  templateUrl: "search.component.html"
})
export class SearchComponent {

  result : any[] = []; 
  searchForWord: FormControl = new FormControl();
  currentSearch: FormControl = new FormControl();
  dataLoaded : boolean = false;


  get suras() : Sura[]{
    return this.repo.suras.getValue();
  }
  constructor(private repo: Repository, private stateService : StateService) {
       
    this.repo.quranClean.subscribe(data => this.dataLoaded = data.length > 1);
    stateService.pipe(skipWhile(newState  => this.checkLocalStateChange(newState)))
    .subscribe(newState => {
      this.currentSearch.setValue(newState["currentSearch"], {emitEvent :false});
      this.searchForWord.setValue(newState["searchForWord"], {emitEvent :false});
      this.setResult(this.currentSearch.value);
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

  checkLocalStateChange(newState: any) : boolean{
    return ( newState["currentSearch"] == this.currentSearch.value &&
             newState["searchForWord"] == this.searchForWord.value);  }
   

  setResult(word :string){ 
    if(this.searchForWord.value === true){
      this.result = word.length > 1 ? this.repo.trie.find(word) : [];
    }
    else{
        if(word.length > 1){
          this.repo.quranClean.subscribe(data=>this.result = data.filter(q => q.text.includes(word)));                                              
        }
        else
        {
          this.result = [];
        }
    }
  }
}
