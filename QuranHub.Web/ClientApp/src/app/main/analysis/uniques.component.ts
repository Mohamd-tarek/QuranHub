import { Component } from '@angular/core';
import { QuranRepository } from "../../abstractions/repositories/quranRepository";
import { Sura } from '../../models/meta/sura.model';
import { Quran } from 'src/app/models/quran/quran.model';

@Component({
  selector: "Uniques",
  templateUrl: "uniques.component.html"
})

export class UniquesComponent  {
  
  suraLoaded :boolean = false;
  ayaLoaded :boolean = false;
  result: Quran[] = [];

  constructor(private repo: QuranRepository ) {

    this.repo.suras.subscribe((data:any) => {
      this.suraLoaded = data.length > 1;
    });

    this.repo.getUniques().subscribe(data => {
      if (data.length > 0){
        this.ayaLoaded = true;
        this.result = data;
      }
    });
    
  }

  get dataLoaded() :boolean{
    return (this.suraLoaded && this.ayaLoaded)
  }

  get suras(): Sura[] {
    return this.repo.suras.getValue();
  }

}
