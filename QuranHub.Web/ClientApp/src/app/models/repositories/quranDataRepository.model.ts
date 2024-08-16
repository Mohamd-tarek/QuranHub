import { Quran } from "../quran/quran.model";
import { Sura } from "../meta/sura.model";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Trie } from "../trie";
import { Observable, BehaviorSubject, Subject } from "rxjs";
import { Note } from "../quran/note.model";
import { QuranRepository } from "../../abstractions/repositories/quranRepository";
import { restApiPaths } from "../../constants/quran.constants";
import { DataWraper } from "../dataWraper.model";
import { CommonFuntionality } from "../commonFunctionality.service";

@Injectable({
  providedIn: 'root',
})    

export class QuranDataRepository extends QuranRepository { 

  private _words = new Map<string, number>();
  private _letters = new Map<string, number>();
  private _trie = new Trie();
  private _mindMapCache: Map<number, []> = new Map<number,any>();

  
  quran: DataWraper<Quran> = new DataWraper<Quran>(this.http, restApiPaths.QuranInfoURLS.Quran, this.commonFunctionality.aggregateSura);

  quranClean: DataWraper<Quran> = new DataWraper<Quran>(this.http, restApiPaths.QuranInfoURLS.QuranClean);

  muyassar: DataWraper<Quran> = new DataWraper<Quran>(this.http, restApiPaths.QuranInfoURLS.Muyassar, this.commonFunctionality.aggregateSura);

  tabary: DataWraper<Quran> = new DataWraper<Quran>(this.http, restApiPaths.QuranInfoURLS.Tabary, this.commonFunctionality.aggregateSura);

  qortobi: DataWraper<Quran> = new DataWraper<Quran>(this.http, restApiPaths.QuranInfoURLS.Qortobi, this.commonFunctionality.aggregateSura);

  ibnKatheer: DataWraper<Quran> = new DataWraper<Quran>(this.http, restApiPaths.QuranInfoURLS.IbnKatheer, this.commonFunctionality.aggregateSura);

  jalalayn: DataWraper<Quran> = new DataWraper<Quran>(this.http, restApiPaths.QuranInfoURLS.Jalalayn, this.commonFunctionality.aggregateSura);

  translation: DataWraper<Quran> = new DataWraper<Quran>(this.http, restApiPaths.QuranInfoURLS.Translation, this.commonFunctionality.aggregateSura);

  suras: DataWraper<Sura> = new DataWraper<Sura>(this.http, restApiPaths.QuranInfoURLS.Suras);

  words: BehaviorSubject<Map<string, number>> = new BehaviorSubject<Map<string, number>>(new Map<string, number>());

  letters: BehaviorSubject<Map<string, number>> = new BehaviorSubject<Map<string, number>>(new Map<string, number>());

  trie: BehaviorSubject<Trie> =new BehaviorSubject<Trie>(new Trie());

  constructor(private http: HttpClient, private commonFunctionality: CommonFuntionality) {
         super();
         this.countWords();
         this.countLetters();
  }  
  
  getMindMap(index : number) :Observable<any> {
    let subject = new Subject<[]>();
    if(this._mindMapCache.has(index)){
         setTimeout(()=>subject.next(this._mindMapCache.get(index) as []),50);
    }
    else{
         this.http.get<any>(restApiPaths.MindMapURL  + index).subscribe(data => {
          this._mindMapCache.set(index, data);
          subject.next(data);
         })
    }

    return subject;
  }
 
  getNote(aya : Quran) :Observable<Note> {
     return this.http.get<Note>(restApiPaths.NoteUrl  + aya.index);
   }

  insertNote(note : Note)  {
    note.noteId = 0;
    return this.http.post(restApiPaths.CreateNoteUrl, note).subscribe(response=> {
    });
  }
   
  getSimilarAyas(aya : Quran) :Observable<Quran[]> {
    return this.http.get<Quran[]>(restApiPaths.AnalysisURL + "similar-ayas/" + aya.index);
  }

  getUniques() :Observable<Quran[]> {
    return this.http.get<Quran[]>(restApiPaths.AnalysisURL  + "uniques");
  }

  getTopics() :Observable<Quran[][]> {
    return this.http.get<Quran[][]>(restApiPaths.AnalysisURL  + "topics");
  }

  private  countWords() {   
      this.quranClean.subscribe( data=>{ 
        data.forEach(aya => {
        let curWords = aya.text.split(" ");
        this.insertCollection(curWords, this._words);
      });
      this.words.next(this._words);

        this.buildTrie();
    });
  }

  private buildTrie():void {
    let words = this.words.getValue();
    for(const key of words.keys())
    {
      this._trie.insert(key, words.get(key));
    }
    this.trie.next(this._trie);
  }

  private countLetters() {   
    this.quranClean.subscribe(data =>{ data.forEach(aya => {
      let curLetters = aya.text.split("");
      this.insertCollection(curLetters, this._letters);
      })
      this.letters.next(this._letters);
    });
  } 
 
  private insertCollection(dataToInsert : string[], container: Map<string, number> ){
      dataToInsert.forEach(item => container.has(item) ? 
      container.set(item, container.get(item) as number + 1) :
      container.set(item, 1) );
  }

}
