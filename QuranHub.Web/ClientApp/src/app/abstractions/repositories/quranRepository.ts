import { Quran } from "../../models/quran/quran.model";
import { Observable} from "rxjs";
import { Note } from "../../models/quran/note.model";

export abstract class QuranRepository {
  
  quran : any;
  quranClean : any;
  muyassar : any; 
  tabary : any;
  qortobi : any;
  ibnKatheer : any;
  jalalayn : any;
  translation : any; 
  suras : any;
  words: any; 
  letters: any; 
  trie: any;
  
  abstract getMindMap(index : number) :Observable<any>; 
 
  abstract getNote(aya : Quran) :Observable<Note> ;

  abstract insertNote(note : Note): void; 
  
  abstract getSimilarAyas(aya : Quran) :Observable<Quran[]>;
  
  abstract getUniques() :Observable<Quran[]>;

  abstract getTopics() :Observable<Quran[][]>;
}
