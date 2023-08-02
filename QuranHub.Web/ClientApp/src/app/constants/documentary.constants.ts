
import { RestApiUrl } from "./application.constants";

export const documentaryURL = RestApiUrl + "/Documentary";


export const VideoActions = {
  LoadMoreComments: `LoadMoreComments`,
  LoadMoreVideoInfoReacts: `LoadMoreReacts`,
  LoadMoreCommentReacts: `LoadMoreCommentReacts`,
  AddVideoInfoReact: `AddReact`,
  RemoveVideoInfoReact: `RemoveReact`,
  AddCommentReact: `AddCommentReact`,
  RemoveCommentReact: `RemoveCommentReact`,
  AddComment: `AddComment`,
  RemoveComment: `RemoveComment`,
};

interface DocumentaryPathsType  {
  readonly PlayListsInfo: string;
  readonly PlayListInfo: string;
  readonly VideoInfoForPlayList : string;
  readonly VideoInfo: string;
  readonly Video: string;
  readonly Verses: string;
  readonly LoadMoreComments: string;
  readonly LoadMoreVideoInfoReacts: string;
  readonly LoadMoreCommentReacts: string;
  readonly AddVideoInfoReact: string;
  readonly RemoveVideoInfoReact: string;
  readonly AddComment: string;
  readonly RemoveComment: string;
  readonly AddCommentReact: string;
  readonly RemoveCommentReact: string;

}

export let documentaryPaths: DocumentaryPathsType = {
  PlayListsInfo: `${documentaryURL}/PlayListsInfo/`,
  PlayListInfo: `${documentaryURL}/PlayListInfo/`,
  VideoInfoForPlayList: `${documentaryURL }/VideoInfoForPlayList/`,
  VideoInfo: `${documentaryURL}/VideoInfo/`,
  Video: `${documentaryURL}/Video/`,
  Verses: `${documentaryURL}/Verses/`,
  LoadMoreComments: `${documentaryURL}/${VideoActions.LoadMoreComments}/`,
  LoadMoreVideoInfoReacts: `${documentaryURL}/${VideoActions.LoadMoreVideoInfoReacts}/`,
  LoadMoreCommentReacts: `${documentaryURL}/${VideoActions.LoadMoreCommentReacts}/`,
  AddVideoInfoReact: `${documentaryURL}/${VideoActions.AddVideoInfoReact}/`,
  RemoveVideoInfoReact: `${documentaryURL}/${VideoActions.RemoveVideoInfoReact}/`,
  AddComment: `${documentaryURL}/${VideoActions.AddComment}/`,
  RemoveComment: `${documentaryURL}/${VideoActions.RemoveComment}/`,
  AddCommentReact: `${documentaryURL}/${VideoActions.AddCommentReact}/`,
  RemoveCommentReact: `${documentaryURL}/${VideoActions.RemoveCommentReact}/`,
}







