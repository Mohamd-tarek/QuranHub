
import { RestApiUrl } from "./application.constants";

export const dataURL = RestApiUrl + "/hadith";


interface HadithPathsType  {
   readonly SahihElbokhary : string;
}


export const  hadithURLS : HadithPathsType = {
   SahihElbokhary : `${dataURL}/get-all/`,
}








