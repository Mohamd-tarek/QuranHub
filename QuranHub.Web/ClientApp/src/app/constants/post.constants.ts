import { RestApiUrl } from "./application.constants";

export const postURL = RestApiUrl + "/post";

export const PostActions = {
   LoadMoreComments: `load-more-comments`,
   LoadMorePostReacts: `load-more-post-reacts`,
   LoadMoreCommentReacts: `load-more-comment-reacts`,
   LoadMoreShares: `load-more-shares`,
   AddPostReact: `add-post-react`,
   RemovePostReact : `remove-post-react`,
   AddCommentReact : `add-comment-react`,
   RemoveCommentReact : `add-comment-react`,
   AddComment: `add-comment`,
   RemoveComment: `remove-comment`,
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
  GetPostById: `${postURL}/get-post-by-id/`,
  Verses: `${postURL}/verses/`,
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
  SharePost: `${postURL}/share-post/`,
  UnSharePost: `${postURL}/unSharePost/`,
  EditPost: `${postURL}/edit-post/`,
  DeletePost: `${postURL}/delete-post/`,
 
}

