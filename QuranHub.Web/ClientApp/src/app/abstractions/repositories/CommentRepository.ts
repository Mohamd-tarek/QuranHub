import { Observable} from "rxjs";
import { Comment } from "../../models/post/comment.model";
import { React } from "../../models/post/react.model";


export abstract class CommentRepository {

  abstract loadMoreComments(commentableId: number, offset: number, size: number): Observable<Comment[]>;

  abstract loadMoreCommentReacts(CommentId: number, offset: number, size: number): Observable<React[]>

  abstract addComment(comment: string, quranHubUserId: string, commentableId:number, verseId : number | null): Observable<Comment>;

  abstract removeComment(CommentId: number): Observable<any>;

  abstract addCommentReact(type: number, commentId: number, quranHubUserId: string): Observable<React>;

  abstract removeCommentReact(commentId: number): Observable<any>;

}
