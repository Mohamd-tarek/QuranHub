
import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { hadithURLS } from "src/app/constants/hadith.constants";
import { DataWraper } from "../dataWraper.model";
import { HadithRepository } from "src/app/abstractions/repositories/hadithRepository";
import { Section } from "../hadith/section.model";

@Injectable({
  providedIn: 'root',
})    

export class HadithDataRepository extends HadithRepository { 

  
  sahihElbokhary: DataWraper<Section> = new DataWraper<Section>(this.http, hadithURLS.SahihElbokhary);

  constructor(private http: HttpClient) {
         super();
  }  
}
