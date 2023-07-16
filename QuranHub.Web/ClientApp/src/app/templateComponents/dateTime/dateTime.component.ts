import { Component, Input, OnInit } from '@angular/core';
import { switchAll } from 'rxjs';

@Component({
  selector: "dateTime",
  templateUrl: "dateTime.component.html"
})

export class DateTimeComponent implements OnInit {

  @Input()
  dateTime: string = "";

  elapsedTime: number = 0;
  elapsedTimeUnit: string = "s";

  ngOnInit() {
    let currDatetime = new Date();
    let targetDate = new Date(this.dateTime); 

    if (currDatetime.getFullYear() > targetDate.getFullYear()) {
      this.elapsedTime = currDatetime.getFullYear() - targetDate.getFullYear();
      this.elapsedTimeUnit = "y";

    } else if (currDatetime.getMonth() > targetDate.getMonth()) {
      this.elapsedTime = currDatetime.getMonth() - targetDate.getMonth();
      this.elapsedTimeUnit = "mths";

    } else if (currDatetime.getDate() > targetDate.getDate()) {
      this.elapsedTime = currDatetime.getDate() - targetDate.getDate();
      this.elapsedTimeUnit = "d";

    } else if (currDatetime.getHours() > targetDate.getHours()) {
      this.elapsedTime = currDatetime.getHours() - targetDate.getHours();
      this.elapsedTimeUnit = "h";

    } else if (currDatetime.getMinutes() > targetDate.getMinutes()) {
      this.elapsedTime = currDatetime.getMinutes() - targetDate.getMinutes();
      this.elapsedTimeUnit = "m";

    } else {
      this.elapsedTime = currDatetime.getSeconds() - targetDate.getSeconds();
      this.elapsedTimeUnit = "s";
    }

  }
  
}
