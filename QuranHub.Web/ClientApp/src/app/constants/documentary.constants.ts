
import { RestApiUrl } from "./application.constants";

export const documentaryURL = RestApiUrl + "/Documentary";

interface DocumentaryPathsType  {
  readonly PlayListsInfo: string;
  readonly PlayListInfo: string;
   readonly VideoInfoForPlayList : string;
   readonly VideoInfo : string; 
   readonly Video : string;

}

export let documentaryPaths: DocumentaryPathsType = {
  PlayListsInfo: `${documentaryURL}/PlayListsInfo/`,
  PlayListInfo: `${documentaryURL}/PlayListInfo/`,
  VideoInfoForPlayList: `${documentaryURL }/VideoInfoForPlayList/`,
  VideoInfo: `${documentaryURL}/VideoInfo/`,
  Video: `${documentaryURL}/Video/`,
}







