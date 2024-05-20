
import { RestApiUrl } from "./application.constants";

export const documentaryURL = RestApiUrl + "/documentary";


export const VideoActions = {
  LoadMoreComments: `load-more-comments`,
  LoadMoreVideoInfoReacts: `load-more-reacts`,
  LoadMoreCommentReacts: `load-more-comments-reacts`,
  AddVideoInfoReact: `add-react`,
  RemoveVideoInfoReact: `remove-react`,
  AddCommentReact: `add-comment-react`,
  RemoveCommentReact: `remove-comment-react`,
  AddComment: `add-comment`,
  RemoveComment: `remove-comment`,
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
  PlayListsInfo: `${documentaryURL}/play-lists-info/`,
  PlayListInfo: `${documentaryURL}/play-list-info/`,
  VideoInfoForPlayList: `${documentaryURL }/video-info-for-play-list/`,
  VideoInfo: `${documentaryURL}/video-info/`,
  Video: `${documentaryURL}/video/`,
  Verses: `${documentaryURL}/verses/`,
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







