import { Observable} from "rxjs";
import { Comment } from "../../models/post/comment.model";
import { PostReact } from "../../models/post/postReact.model";
import { CommentReact } from "../../models/post/commentReact.model";
import { Share } from "../../models/post/share.model";
import { Post } from "../../models/post/post.model";

export abstract class PostRepository {

  verses: any;
  
  abstract getPostById(postId: number): Observable<Post>;

  abstract getPostByIdWithSpecificComment(postId: number, commentId:number): Observable<Post>;

  abstract loadMoreComments(postId: number, offset: number, size: number): Observable<Comment[]>;

  abstract loadMorePostReacts(postId: number, offset: number, size: number): Observable<PostReact[]>;

  abstract loadMoreCommentReacts(CommentId: number, offset: number, size: number): Observable<CommentReact[]>

  abstract loadMoreShares(postId: number, offset: number, size: number): Observable<Share[]>;

  abstract addPostReact(type : number, postId: number): Observable<PostReact>;

  abstract removePostReact(postId: number): Observable<any>;

  abstract addComment(comment: string, quranHubUserId: string, postId:number, verseId : number | null): Observable<Comment>;

  abstract removeComment(CommentId: number): Observable<any>;

  abstract addCommentReact(type: number, postCommentId: number, quranHubUserId: string): Observable<CommentReact>;

  abstract removeCommentReact(postCommentId: number): Observable<any>;

  abstract sharePost(verseId: number, QuranHubUserId: string, text: string, privacy: string, postId: number): Observable<Share>;

  abstract unSharePost(shareId: number): Observable<boolean>;

  abstract editPost(postId: number, postVerseId: number, text: string, privacy: string): Observable<true>;

  abstract deletePost(postId: number): Observable<any>;
}
