import { Quran } from "./quran/quran";
import { Tafseer } from "./quran/tafseer";
import { Translation } from "./quran/translation";
import { Hizb } from "./meta/hizb";
import { Juz } from "./meta/juz";
import { Manzil } from "./meta/manzil";
import { Page } from "./meta/page";
import { Ruku } from "./meta/ruku";
import { Sajda } from "./meta/sajda";
import { Sura } from "./meta/sura";
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Trie } from "./trie";
import { Observable } from "rxjs";
import { Note } from "./quran/note";
import { catchError } from "rxjs/operators";




@Injectable({
  providedIn: 'root',
})

export class Repository {
  apiURL: string = "https://localhost:5001/api/Data/";
  sessionURL: string = "https://localhost:5001/api/Session/";
  private _quran: Quran[] = [];
  private _tafseer: Tafseer[] = [];
  private _translation: Translation[] = [];
  private _quranClean: Quran[] = [];
  private _hizbs: Hizb[] = [];
  private _juzs: Juz[] = [];
  private _manzils: Manzil[] = [];
  private _pages: Page[] = [];
  private _rukus: Ruku[] = [];
  private _sajdas: Sajda[] = [];
  private _suras: Sura[] = [];
  private _words = new Map<string, number>();
  private _letters = new Map<string, number>();
  private _trie = new Trie();
  
  get quran(): Quran[] {
    if(this._quran.length === 0)
    {
       this.http.get<Quran[]>(this.apiURL + "Quran").subscribe(q => this._quran = q);   
    }
    return this._quran;

  }

  get tafseer(): Tafseer[] {
    if(this._tafseer.length === 0)
    {
      this.http.get<Tafseer[]>(this.apiURL + "Tafseer").subscribe(t => this._tafseer = t);
    }
    return this._tafseer;
  } 

  get translation(): Translation[] {
    if(this._translation.length === 0)
    {
      this.http.get<Translation[]>(this.apiURL + "Translation").subscribe(t => this._translation = t);
    }
    return this._translation;

  }

  get quranClean(): Quran[] {
    return this._quranClean;
  }

  get hizbs(): Hizb[] {
    if(this._hizbs.length === 0){
      this.http.get<Hizb[]>(this.apiURL + "Hizbs").subscribe(h => this._hizbs = h);
    }
    return this._hizbs;
  }

  get juzs(): Juz[] {
    if(this._juzs.length === 0){
      this.http.get<Juz[]>(this.apiURL + "Juzs").subscribe(j => this._juzs = j);
    }
    return this._juzs;
  }

  get manzils(): Manzil[] {
    if(this._manzils.length === 0){
      this.http.get<Manzil[]>(this.apiURL + "Manzils").subscribe(m => this._manzils = m);
    }
    return this._manzils;
  }

  get pages(): Page[] {
    if(this._pages.length === 0){
      this.http.get<Page[]>(this.apiURL + "Pages").subscribe(p => this._pages = p);
    }
    return this._pages;
  }

  get rukus(): Ruku[] {
    if(this._rukus.length === 0){
      this.http.get<Ruku[]>(this.apiURL + "Rukus").subscribe(r => this._rukus = r);
    }
    return this._rukus;
  }

  get sajdas(): Sajda[] {
    if(this._sajdas.length === 0){
      this.http.get<Sajda[]>(this.apiURL + "Sajdas").subscribe(s => this._sajdas = s);
    }
    return this._sajdas;
  }

  get suras(): Sura[] {
    if(this._suras.length === 0){
      this.http.get<Sura[]>(this.apiURL + "Suras").subscribe(s => this._suras = s);
    }
    return this._suras;
  }

  get words(): Map<string, number>{
    return this._words;
  } 

  get letters(): Map<string, number>{
    return this._letters;
  }

  get trie() : Trie{
    return this._trie;
  }

  constructor(private http: HttpClient) {
    
      this.http.get<Quran[]>(this.apiURL + "QuranClean").subscribe(q =>{ this._quranClean = q; this.countWords(); this.countLetters();});
  
  }
 
  getNote(aya : Quran) :Observable<Note> {
     return this.http.get<Note>(this.apiURL + "Note" + '/' + aya.index);
   }

  insertNote(note : Note)  {
    return this.http.post(this.apiURL + "Note", note).subscribe(response=> {
      console.log(response);
    });
  }

  storeSessionData<T>(dataType : string, data: T){
             return this.http.post(this.sessionURL + dataType,data).subscribe(response=> {});
  }
  
  getSessionData<T>(dataType : string) : Observable<T>{
           return this.http.get<T>(this.sessionURL + dataType);
  }

  login(name: string, password: string ) : Observable<boolean>{
        return this.http.post<boolean>("/api/account/login", 
        {name: name, password: password});
  }

  logout() {
    return this.http.post<boolean>("/api/account/logout", null).subscribe(response => {});
}

  countWords() {   
      this.quranClean.forEach(aya => {
        let curWords = aya.text.split(" ");
        curWords.forEach(word => this.words.has(word) ? 
                          this.words.set(word, this.words.get(word) as number + 1) 
                          : this.words.set(word, 1) )
      });

      for(const key of this.words.keys())
      {
        this.trie.insert(key, this.words.get(key));
      }
  }

  

  countLetters() {   
    this.quranClean.forEach(aya => {
      let curLetters = aya.text.split("");
      curLetters.forEach(letter => this.letters.has(letter) ? 
                            this.letters.set(letter, this.letters.get(letter) as number + 1) :
                            this.letters.set(letter, 1) )
    });
  }
}
