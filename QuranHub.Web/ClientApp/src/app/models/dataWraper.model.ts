import { HttpClient } from "@angular/common/http";
import { BehaviorSubject} from "rxjs";

export class DataWraper<T> extends BehaviorSubject<T[]>  {

  constructor(private http: HttpClient, private _url: string, converter: any = null) {

    super([]);

    this.http.get<T[]>(this._url).subscribe(data => {

    if(converter != null)
    {
      data = converter(data);
    }

    super.next(data)});   
  } 
}




