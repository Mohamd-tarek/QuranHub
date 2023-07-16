
import { RestApiUrl } from "./application.constants";

export const dataURL = RestApiUrl + "/Quran";

export const quranInfoURL = RestApiUrl + "/Quran/QuranInfo";

interface QuranInfoPathsType  {
   readonly Quran : string;
   readonly QuranClean : string;
   readonly Muyassar : string; 
   readonly Tabary : string;
   readonly Qortobi : string;
   readonly IbnKatheer : string;
   readonly Jalalayn : string;
   readonly Translation : string; 
   readonly Suras : string;
}

interface RestApiPathsType  {
   readonly QuranInfoURLS:  QuranInfoPathsType,
   readonly MindMapURL: string,
   readonly NoteUrl: string,
   readonly AnalysisURL: string,
   }

let quranInfoURLS : QuranInfoPathsType = {
   Quran : `${quranInfoURL}/Quran/`,
   QuranClean : `${ quranInfoURL }/QuranClean/`,
   Muyassar: `${quranInfoURL}/Muyassar/`,
   Tabary :`${ quranInfoURL }/Tabary/`,
   Qortobi: `${quranInfoURL}/Qortobi/`,
   IbnKatheer : `${ quranInfoURL }/IbnKatheer/`,
   Jalalayn: `${quranInfoURL}/Jalalayn/`,
   Translation :`${ quranInfoURL }/Translation/`,
   Suras: `${quranInfoURL}/Suras/`
}

export const restApiPaths: RestApiPathsType = {
   QuranInfoURLS: quranInfoURLS,
   MindMapURL: `${dataURL }/MindMap/`,
   NoteUrl : `${ dataURL }/Note/`,
   AnalysisURL: `${RestApiUrl}/Analysis/`
}







