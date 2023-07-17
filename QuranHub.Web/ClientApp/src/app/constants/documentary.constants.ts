
import { RestApiUrl } from "./application.constants";

export const documentaryURL = RestApiUrl + "/Documentary";

interface DocumentaryPathsType  {
   readonly PlayListsInfo : string;
   readonly VideoInfoForPlayList : string;
   readonly VideoInfo : string; 
   readonly Video : string;

}

export let documentaryPaths: DocumentaryPathsType = {
  PlayListsInfo: `${documentaryURL}/PlayListsInfo/`,
  VideoInfoForPlayList: `${documentaryURL }/VideoInfoForPlayList/`,
  VideoInfo: `${documentaryURL}/VideoInfo/`,
  Video: `${documentaryURL}/Video/`,
}







