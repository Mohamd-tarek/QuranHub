import { Injectable } from "@angular/core";
import { HttpClient, HttpParams } from "@angular/common/http";
import { Observable } from "rxjs";
import { PostRepository } from "../../abstractions/repositories/postRepository";
import { DataWraper } from "../dataWraper.model";
import { Quran } from "../quran/quran.model";
import { postPaths } from "../../constants/post.constants";
import { Post } from "../post/post.model";
import { PostReact } from "../post/postReact.model";
import { CommentReact } from "../post/commentReact.model";
import { Share } from "../post/share.model";
import { Comment } from "../post/comment.model";
import { CommonFuntionality } from "../commonFunctionality.service";

@Injectable({
  providedIn: 'root',
})    

export class PostDataRepository extends PostRepository { 

  constructor(private http: HttpClient, private commonFunctionality: CommonFuntionality) {
    super();
  }  

  verses: DataWraper<Quran> = new DataWraper<Quran>(this.http, postPaths.Verses, this.commonFunctionality.aggregateSura);

  getPostById(postId: number): Observable<Post> {
    return this.http.get<Post>(postPaths.GetPostById + postId );
  }

  getPostByIdWithSpecificComment(postId: number, commentId:number): Observable<Post> {
    return this.http.get<Post>(postPaths.GetPostById + postId + '/' + commentId );
  }

  loadMoreComments(postId: number, offset: number, size: number = 50): Observable<Comment[]> {
   return  this.http.get<Comment[]>(postPaths.LoadMoreComments + postId + '/' + offset + '/' + size); 
  }

  loadMorePostReacts(postId: number, offset: number, size: number = 50): Observable<PostReact[]> {
    return this.http.get<PostReact[]>(postPaths.LoadMorePostReacts + postId + '/' + offset + '/' + size);
  }

  loadMoreCommentReacts(CommentId: number, offset: number, size: number = 50): Observable <CommentReact[]> {
    return this.http.get<CommentReact[]>(postPaths.LoadMoreCommentReacts + CommentId + '/' + offset + '/' + size);
  }

  loadMoreShares(postId: number, offset: number, size: number = 50): Observable <Share[]> {
    return this.http.get<Share[]>(postPaths.LoadMoreShares + postId + '/' + offset + '/' + size);
  }

  addPostReact(type : number, postId: number): Observable<PostReact> {
    return this.http.post<PostReact>(postPaths.AddPostReact, {
      Type : type,
      PostId : postId
    });
  }

  removePostReact(postId: number): Observable<any> {
    let httpParams = new HttpParams().set('postId', postId);
    let options = { params: httpParams };

    return this.http.delete(postPaths.RemovePostReact, options);
  }

  addComment(comment: string, quranHubUserId: string, postId: number, verseId:number | null): Observable<Comment> {
    return this.http.post<Comment>(postPaths.AddComment, {
      quranHubUserId: quranHubUserId,
      postId: postId,
      text: comment,
      verseId: verseId
    });
  }

  removeComment(CommentId: number): Observable<any> {
    let httpParams = new HttpParams().set('CommentId', CommentId);
    let options = { params: httpParams };

    return this.http.delete(postPaths.RemoveComment, options);
  }

  addCommentReact(type: number, CommentId: number, quranHubUserId: string): Observable<CommentReact> {
    return this.http.post<CommentReact>(postPaths.AddCommentReact, {
      Type : type,
      CommentId: CommentId,
      quranHubUserId: quranHubUserId,
    });
  }

  removeCommentReact(CommentId: number): Observable<any> {
    let httpParams = new HttpParams().set('CommentId', CommentId);
    let options = { params: httpParams };

    return this.http.delete(postPaths.RemoveCommentReact, options);
  }

  sharePost(verseId: number, QuranHubUserId: string, text: string, privacy: string, postId: number): Observable<Share> {
    return this.http.post<Share>(postPaths.SharePost, {
      verseId: verseId,
      quranHubUserId: QuranHubUserId,
      text: text,
      privacy: Number(privacy),
      share: {
        postId: postId,
      }
    });
  }

  unSharePost(shareId: number): Observable<boolean> {
    return this.http.post<boolean>(postPaths.UnSharePost, {
      shareId: shareId,
    });

  }

  editPost(postId: number, verseId: number, text: string, privacy: string): Observable<true> {
    return this.http.put<true>(postPaths.EditPost, {
      postId: postId,
      verseId: verseId,
      text: text,
      privacy: Number(privacy)
    });
  }

  deletePost(postId: number): Observable<any> {
    let httpParams = new HttpParams().set('postId', postId);
    let options = { params: httpParams };

    return this.http.delete(postPaths.DeletePost, options);
  }
}
