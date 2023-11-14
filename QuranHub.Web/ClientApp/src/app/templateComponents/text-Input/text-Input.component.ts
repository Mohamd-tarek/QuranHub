import { Component, Input, OnInit, Output } from '@angular/core';
import { FormControl } from '@angular/forms';
import { EventEmitter } from '@angular/core';

@Component({
  selector: "text-Input",
  templateUrl: "text-Input.component.html"
})

export class TextInputComponent implements OnInit  {

  @Input()
  initialValue: string = "";

  keyWord: FormControl = new FormControl();

  @Output()
  change = new EventEmitter<string>();

  constructor(){
    
      this.keyWord.valueChanges.subscribe(()=> { 
          this.change.emit(this.keyWord.value);
           });
  }

  ngOnInit(): void {

    this.keyWord.setValue(this.initialValue);
  }

}
