import { Injectable } from "@angular/core";

@Injectable({
  providedIn: 'root',
})    

export class CommonFuntionality { 

  public aggregateSura(data: any): any{

    let result: any[114][] = [];

    data.forEach((ele: any) => {

          if(result[ele.sura] == undefined )
          {
                result[ele.sura] = [];  
          }
              result[ele.sura].push(ele);
    });

    return result;
  }
}
