import { RestApiUrl } from "./application.constants";

export const postURL = RestApiUrl + "/Post";

export const PostActions = {
   LoadMoreComments: `LoadMoreComments`,
   LoadMorePostReacts: `LoadMorePostReacts`,
   LoadMoreCommentReacts: `LoadMoreCommentReacts`,
   LoadMoreShares: `LoadMoreShares`,
   AddPostReact: `AddPostReact`,
   RemovePostReact : `RemovePostReact`,
   AddCommentReact : `AddCommentReact`,
   RemoveCommentReact : `RemoveCommentReact`,
   AddComment: `AddComment`,
   RemoveComment: `RemoveComment`,
};

interface PostPathsType  {
      readonly GetPostById: string;
      readonly Verses: string;
      readonly LoadMoreComments: string;
      readonly LoadMorePostReacts: string;
      readonly LoadMoreCommentReacts: string;
      readonly LoadMoreShares: string;
      readonly AddPostReact: string;
      readonly RemovePostReact: string;
      readonly AddComment: string;
      readonly RemoveComment: string;
      readonly AddCommentReact: string;
      readonly RemoveCommentReact: string;
      readonly SharePost: string;
      readonly UnSharePost: string;
      readonly EditPost: string;
      readonly DeletePost: string;
   }

export const postPaths: PostPathsType = {
  GetPostById: `${postURL}/GetPostById/`,
  Verses: `${postURL}/Verses/`,
  LoadMoreComments: `${postURL}/${PostActions.LoadMoreComments}/`,
  LoadMorePostReacts: `${postURL}/${PostActions.LoadMorePostReacts}/`,
  LoadMoreCommentReacts: `${postURL}/${PostActions.LoadMoreCommentReacts}/`,
  LoadMoreShares: `${postURL}/${PostActions.LoadMoreShares}/`,
  AddPostReact: `${postURL}/${PostActions.AddPostReact}/`,
  RemovePostReact: `${postURL}/${PostActions.RemovePostReact}/`,
  AddComment: `${postURL}/${PostActions.AddComment}/`,
  RemoveComment: `${postURL}/${PostActions.RemoveComment}/`,
  AddCommentReact: `${postURL}/${PostActions.AddCommentReact}/`,
  RemoveCommentReact: `${postURL}/${PostActions.RemoveCommentReact}/`,
  SharePost: `${postURL}/SharePost/`,
  UnSharePost: `${postURL}/UnSharePost/`,
  EditPost: `${postURL}/EditPost/`,
  DeletePost: `${postURL}/DeletePost/`,
 
}

