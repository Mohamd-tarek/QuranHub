import { Component } from '@angular/core';
import { StateService } from '../../stateService.service';
import { FormControl } from '@angular/forms';
import { State } from 'src/app/models/state';



@Component({
  selector: "read",
  templateUrl: "read.component.html"
})
export class ReadComponent {
  state :State;
  overviewMode : FormControl; 
  
  constructor(private stateService : StateService){
    this.state = this.stateService.getValue();
    this.overviewMode = new FormControl(this.state.overviewMode);
    this.overviewMode.valueChanges.subscribe(()=>{
      this.state.overviewMode = this.overviewMode.value;
      this.stateService.next(this.state);
    })
  }
   
}
