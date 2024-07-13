import { Component, OnDestroy } from '@angular/core';
import { FormControl } from '@angular/forms';
import { StateService } from '../../abstractions/services/stateService';
import { skipWhile } from 'rxjs/operators';
import {Subscription } from "rxjs";
import { HadithRepository } from 'src/app/abstractions/repositories/hadithRepository';
import { Hadith } from 'src/app/models/hadith/hadith.model';
import { Section } from 'src/app/models/hadith/section.model';


@Component({
  selector: "hadith",
  templateUrl: "hadith.component.html"
})

export class HadithComponent implements OnDestroy {

  subscription: Subscription;
  result : any[] = []; 
  sections: Section[] = [];
  currentSectionIndex : number = 0;


  currentHadithSearch: FormControl = new FormControl();
  dataLoaded : boolean = false;

  constructor(private repo: HadithRepository, private stateService : StateService) {
       
    this.repo.sahihElbokhary.subscribe((data:Section[]) =>{
      this.dataLoaded = data.length > 1;
       this.sections = data;
    });

    this.subscription = this.stateService.pipe(skipWhile((newState:any)  => this.checkLocalStateChange(newState)))
    .subscribe((newState:any) => {
        this.setState(newState);
      });

    this.currentHadithSearch.valueChanges.subscribe(()=> { 
      let state: any  = {"currentHadithSearch": this.currentHadithSearch.value,
                         "currentSectionIndex": this.currentSectionIndex
      }
      this.stateService.next(state);
      });    
  }


  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  checkLocalStateChange(newState: any) : boolean{
    return ( newState["currentHadithSearch"] == this.currentHadithSearch.value ); 
  }
  
  setState(newState: any) : void{
    this.currentHadithSearch.setValue(newState["currentHadithSearch"], {emitEvent :false});
    this.setResult(this.currentHadithSearch.value);
    this.currentSectionIndex = newState["currentSectionIndex"];
    this.setSection();
  }
   

  setResult(word :string){ 
      if(word.length > 2){
        this.repo.sahihElbokhary.subscribe((data:any) =>{

          let allHadiths:any[] = [];

          data.forEach((section:any) => {
            section.hadiths.forEach((hadith:any) =>  allHadiths.push(hadith))
          });

          this.result = allHadiths.filter((q:any) => q.text.includes(word))
        });                                              
      }
      else
      {
        this.result = [];
      }  
  }

  setSection(){ 
      this.repo.sahihElbokhary.subscribe((data:any) =>{
        this.result = data[this.currentSectionIndex-1].hadiths;

      });                                              
  
  }

  get curSection(): number { return this.currentSectionIndex; }

  set curSection(value: number) {
    this.currentSectionIndex =  value;
    let state: any  = {"currentSectionIndex": this.currentSectionIndex };
    this.stateService.next(state);  
    this.setSection();
  }

  removeItem(index: number): void{
    this.result.splice(index, 1);
  }
 
}
