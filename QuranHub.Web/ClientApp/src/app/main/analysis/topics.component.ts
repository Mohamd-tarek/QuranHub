import { Component } from '@angular/core';
import { QuranRepository } from "../../abstractions/repositories/quranRepository";
import { Sura } from '../../models/meta/sura.model';
import { Quran } from 'src/app/models/quran/quran.model';

@Component({
  selector: "Topics",
  templateUrl: "topics.component.html"
})

export class TopicsComponent  {

  numberOfTopics: number = 0;
  topicNumber: number = 0;
  suraLoaded :boolean = false;
  ayaLoaded :boolean = false;
  topics: Quran[][] = [];
  topic: Quran[] =[];

  get dataLoaded() :boolean{
       return (this.suraLoaded && this.ayaLoaded)
  }
  
  constructor(private repo: QuranRepository ) {

    this.repo.suras.subscribe((data:any) => {
      this.suraLoaded = data.length > 1;
    });

    this.repo.getTopics().subscribe(data => {
      if (data.length > 0){
        this.numberOfTopics = data.length;
        this.ayaLoaded = true;
        this.topics = data;
        this.topic = this.topics[this.topicNumber];
      }
    });
  }

  get curTopicNumber(): number{
    return this.topicNumber;
  }

  set curTopicNumber(value :number){
    this.topicNumber = value;
    this.topic = this.topics[this.topicNumber]
  }
  
  get suras(): Sura[] {
    return this.repo.suras.getValue();
  }

  get TopicsCount(): number []{
    let topicsNumbers = [];

    for (let i = 0; i < this.numberOfTopics; ++i){
      topicsNumbers.push(i);
    }
    return topicsNumbers;
  }

  next(){
    if (this.curTopicNumber < this.numberOfTopics){
      this.curTopicNumber++;
    }
 }

  prev(){
    if (this.curTopicNumber > 1 ){
      this.curTopicNumber--;
    }
  }
}
