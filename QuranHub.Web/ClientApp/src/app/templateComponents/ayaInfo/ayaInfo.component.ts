import { Component, Input, Output, EventEmitter } from '@angular/core';
import { QuranRepository } from 'src/app/abstractions/repositories/quranRepository';
import { Sura } from 'src/app/models/meta/sura.model';
import { Router } from '@angular/router';
import { StateService } from 'src/app/abstractions/services/stateService';

@Component({
  selector: "ayaInfo",
  templateUrl: "ayaInfo.component.html"
})

export class AyaInfoComponent  {

  @Input()
  suraIndex!: number;

  @Input()
  ayaIndex!: number;

  constructor(private repo: QuranRepository,private router: Router, private stateService:StateService){}

  get suras(): Sura[] {
    return this.repo.suras.getValue();
  }

  navigateToRead() {
    let state: any = { "currentQuranSura": this.suraIndex };
    this.stateService.next(state);
    this.router.navigateByUrl("/read/" + this.ayaIndex);  
  }
}
