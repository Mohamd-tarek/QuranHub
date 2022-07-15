import { Quran } from "./quran/quran";
import { Tafseer } from "./quran/tafseer";
import { Translation } from "./quran/translation";
import { Sura } from "./meta/sura";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Trie } from "./trie";
import { Observable, BehaviorSubject } from "rxjs";
import { Note } from "./quran/note";

class DataWraper<T> extends BehaviorSubject<T[]>  {

  constructor(private http: HttpClient, private _url : string){
   super([]);
   this.http.get<T[]>(this._url ).subscribe(data => super.next(data));   
  } 
}


@Injectable({
  providedIn: 'root',
})

export class Repository {
  apiURL: string = "https://localhost:5001/api/";
  apiDataURL: string = "https://localhost:5001/api/Data/";
  sessionURL: string = "https://localhost:5001/api/Session/";
  authUrl : string = "https://localhost:5001/api/Account/";

  private _words = new Map<string, number>();
  private _letters = new Map<string, number>();
  private _trie = new Trie();

  quran : DataWraper<Quran> = new DataWraper<Quran>(this.http, this.apiDataURL + "Quran" );
  quranClean : DataWraper<Quran> = new DataWraper<Quran>(this.http, this.apiDataURL + "QuranClean" );
  tafseer : DataWraper<Tafseer> = new DataWraper<Tafseer>(this.http, this.apiDataURL + "Tafseer" );
  translation : DataWraper<Translation> = new DataWraper<Translation>(this.http, this.apiDataURL + "Translation" );
  suras : DataWraper<Sura> = new DataWraper<Sura>(this.http, this.apiDataURL + "Suras" );
  words: BehaviorSubject<Map<string, number>> = new BehaviorSubject< Map<string, number>>(new Map<string, number>());
  letters: BehaviorSubject<Map<string, number>> = new BehaviorSubject< Map<string, number>>(new Map<string, number>());
  trie: BehaviorSubject<Trie> = new BehaviorSubject<Trie>(new Trie());

  


  constructor(private http: HttpClient) {
                      this.countWords();
                      this.countLetters();
  }  
  
  
 
  getNote(aya : Quran) :Observable<Note> {
     return this.http.get<Note>(this.apiURL + "Note" + '/' + aya.index);
   }

  insertNote(note : Note)  {
    note.id = 0;
    return this.http.post(this.apiURL + "Note", note).subscribe(response=> {
    });
  }

  storeSessionData<T>(dataType : string, data: T){
             return this.http.post(this.sessionURL + dataType,data).subscribe(response=> {});
  }
  
  getSessionData<T>(dataType : string) : Observable<T>{
           return this.http.get<T>(this.sessionURL + dataType);
  }

  login(email: string, password: string ) : Observable<boolean>{
        return this.http.post<boolean>(this.authUrl + "login", 
        {email: email, password: password});
  }

  logout() {
    return this.http.post<boolean>( this.authUrl + "logout", null).subscribe(response => {});
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

 
 private insertCollection(dataToInsert : string[], container: Map<string, number> ): void{
  dataToInsert.forEach(item => container.has(item) ? 
  container.set(item, container.get(item) as number + 1) :
  container.set(item, 1) );
 }
}
