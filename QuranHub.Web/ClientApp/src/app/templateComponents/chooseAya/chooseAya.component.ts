import { Component, Output, EventEmitter, OnInit, Input } from '@angular/core';
import { QuranRepository } from '../../abstractions/repositories/quranRepository';
import { PostRepository } from '../../abstractions/repositories/postRepository';
import { Quran } from 'src/app/models/quran/quran.model';
import { Sura } from 'src/app/models/meta/sura.model';

@Component({
  selector: "chooseAya",
  templateUrl: "chooseAya.component.html"
})

export class ChooseAyaComponent implements OnInit  {
  
  @Input()
  chosenSura: number = 1;

  @Input()
  chosenAya: number = 1;

  aya!: Quran;

  @Output() chooseAyaEvent = new EventEmitter<Quran>();
  
  constructor (
    public postDataRepository : PostRepository,
    public dataRepository: QuranRepository){  }

  ngOnInit(): void {
    this.chooseAya();
  }

  get curSura(): number {
  return this.chosenSura;
  }
  
  set curSura(value : number) {
    this.chosenSura = value;
    this.curAya = 1;
    this.chooseAya();
  }

  get curAya(): number {
    return this.chosenAya;
  }

  set curAya(value : number) {
      this.chosenAya = value;
      this.chooseAya();
  }


  get suras(): Sura[] {
    return this.dataRepository.suras.getValue();
  }

  get ayas(): number[] {
    
    let ayas = [];

   if(this.suras[this.curSura - 1])
   {
     for (let i = 1; i <= this.suras[this.curSura - 1].ayas; ++i){
       ayas.push(i);
     }
   }
    return ayas;
  }

  chooseAya() {
    this.postDataRepository.verses.subscribe((data: any) => {
      this.aya = data[this.curSura][this.curAya - 1];
      this.chooseAyaEvent.emit(this.aya);
      });
  }

}
