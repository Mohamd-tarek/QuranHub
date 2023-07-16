import { Component, Output, EventEmitter, OnInit, Input } from '@angular/core';

import { Privacy } from 'src/app/models/post/post.model';

@Component({
  selector: "choosePrivacy",
  templateUrl: "choosePrivacy.component.html"
})

export class ChoosePrivacyComponent  {
  
  @Input()
  chosenPrivacy: Privacy = Privacy.Public;
  Privacy = Privacy;
  
  @Output() choosePrivacyEvent = new EventEmitter<Privacy>(); 

  get curPrivacy(): Privacy {
    return this.chosenPrivacy;
  }

  set curPrivacy(value : Privacy) {
      this.chosenPrivacy = value;
      this.choosePrivacy();
     
  }

  choosePrivacy() { 
      this.choosePrivacyEvent.emit(this.chosenPrivacy);
  }
}
