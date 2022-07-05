import { Component } from '@angular/core';
import { StateService } from '../../stateService.service';
import { FormControl } from '@angular/forms';
import { skipWhile } from 'rxjs/operators';



@Component({
  selector: "read",
  templateUrl: "read.component.html"
})
export class ReadComponent {
  overviewMode : FormControl = new FormControl(false); 
  
  constructor(private stateService : StateService){
    stateService.pipe(skipWhile(newState => this.overviewMode.value == newState["overviewMode"]))
    .subscribe(newState => {
         this.overviewMode.setValue(newState["overviewMode"], {emitEvent :false});
      });

    this.overviewMode.valueChanges.subscribe(()=>{
      let state: any = {"overviewMode": this.overviewMode.value}
      this.stateService.next(state);
    })
  }
   
}
