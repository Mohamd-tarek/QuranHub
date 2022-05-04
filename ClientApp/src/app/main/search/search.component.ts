import { Component } from '@angular/core';
import { Repository } from "../../models/repository";
import { FormControl } from '@angular/forms';
import { Sura } from 'src/app/models/meta/sura';
import { StateSevice } from '../stateService.service';

@Component({
  selector: "search",
  templateUrl: "search.component.html"
})
export class SearchComponent {

  result : any[] = []; 
  searchForWord = new FormControl(false);
  word = new FormControl('');
  dataLoaded : boolean;


  get suras() : Sura[]{
    return this.repo.suras;
  }
  constructor(private repo: Repository, private state : StateSevice) {
    this.repo.quranClean;
    this.repo.trie;
    this.repo.suras;
    this.dataLoaded = this.repo.quranClean.length > 0 && this.repo.suras.length > 0;

    this.searchForWord.setValue(this.state.searchForWord);
    this.word.setValue(this.state.currentSearch);
    this.setResult(this.state.currentSearch);

    this.searchForWord.valueChanges.subscribe(()=> { 
      this.state.searchForWord = this.searchForWord.value;
      this.setResult(this.word.value);
   }); 

    this.word.valueChanges.subscribe(()=> { 
       this.state.currentSearch = this.word.value;
       this.setResult(this.word.value);
    });    
  }

  setResult(word :string){
    
    if(this.searchForWord.value === true){
      this.result = word.length > 1 ? this.repo.trie.find(word) : [];
    }
    else{
        this.result = word.length > 1 ? this.repo.quranClean.filter(q => q.text.includes(word)) : [];                                              
    }
    console.log(this.result);
    console.log(this.repo.suras);

  }
}
