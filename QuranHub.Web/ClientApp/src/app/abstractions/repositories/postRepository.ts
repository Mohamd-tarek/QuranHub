import { Observable} from "rxjs";
import { React } from "../../models/post/react.model";
import { Share } from "../../models/post/share.model";
import { Post } from "../../models/post/post.model";
import { CommentRepository } from "./CommentRepository"

export abstract class PostRepository extends CommentRepository {

  verses: any;
  
  abstract getPostById(postId: number): Observable<Post>;

  abstract getPostByIdWithSpecificComment(postId: number, commentId:number): Observable<Post>;

  abstract loadMoreReacts(postId: number, offset: number, size: number): Observable<React[]>;

  abstract loadMoreShares(postId: number, offset: number, size: number): Observable<Share[]>;

  abstract addReact(type : number, postId: number): Observable<React>;

  abstract removeReact(postId: number): Observable<any>;

  abstract sharePost(verseId: number, QuranHubUserId: string, text: string, privacy: string, postId: number): Observable<Share>;

  abstract unSharePost(shareId: number): Observable<boolean>;

  abstract editPost(postId: number, postVerseId: number, text: string, privacy: string): Observable<true>;

  abstract deletePost(postId: number): Observable<any>;
}
