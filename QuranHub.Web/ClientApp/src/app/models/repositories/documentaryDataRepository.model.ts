import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient, HttpParams } from "@angular/common/http";
import { DocumentaryRepository } from "src/app/abstractions/repositories/documentaryRepository";
import { documentaryPaths } from "../../constants/documentary.constants";
import { PlayListInfo } from "src/app/models/video/playListInfo.model";
import { VideoInfo } from "src/app/models/video/VideoInfo.model";
import { React } from "../post/react.model";
import { Comment } from "../post/comment.model";



@Injectable({
  providedIn: 'root',
})

export class DocumentaryDataRepository extends DocumentaryRepository  {

  constructor(private http: HttpClient) {
    super();
  }

  getPlayLists(): Observable<PlayListInfo[]> {
    return this.http.get<PlayListInfo[]>(documentaryPaths.PlayListsInfo);

  }

  getPlayListInfo(playlistName: string): Observable<PlayListInfo> {
    return this.http.get<PlayListInfo>(documentaryPaths.PlayListInfo + playlistName);
  }
    
  getVideoInfoForPlayList(playListName: string, offset: number, amount: number): Observable<VideoInfo[]>
  {
    return this.http.get<VideoInfo[]>(documentaryPaths.VideoInfoForPlayList  + playListName + '/' + offset + '/' + amount);
  }

  GetVideoInfoAsync(name: string): Observable<VideoInfo> {
    return this.http.get<VideoInfo>(documentaryPaths.VideoInfo + name);
  }

  getVideoInfoByIdWithSpecificComment(VideoInfoId: number, commentId: number): Observable<VideoInfo> {
    return this.http.get<VideoInfo>(documentaryPaths.VideoInfo + name + '/' + commentId);
  }

  loadMoreComments(VideoInfoId: number, offset: number, size: number = 50): Observable<Comment[]> {
    return this.http.get<Comment[]>(documentaryPaths.LoadMoreComments + VideoInfoId + '/' + offset + '/' + size);
  }

  loadMoreReacts(VideoInfoId: number, offset: number, size: number = 50): Observable<React[]> {
    return this.http.get<React[]>(documentaryPaths.LoadMoreVideoInfoReacts + VideoInfoId + '/' + offset + '/' + size);
  }

  loadMoreCommentReacts(CommentId: number, offset: number, size: number = 50): Observable<React[]> {
    return this.http.get<React[]>(documentaryPaths.LoadMoreCommentReacts + CommentId + '/' + offset + '/' + size);
  }


  addReact(type: number, VideoInfoId: number): Observable<React> {
    return this.http.post<React>(documentaryPaths.AddVideoInfoReact, {
      Type: type,
      VideoInfoId: VideoInfoId
    });
  }

  removeReact(VideoInfoId: number): Observable<any> {
    let httpParams = new HttpParams().set('VideoInfoId', VideoInfoId);
    let options = { params: httpParams };

    return this.http.delete(documentaryPaths.RemoveVideoInfoReact, options);
  }


  addComment(comment: string, quranHubUserId: string, VideoInfoId: number, verseId: number | null): Observable<Comment> {
    return this.http.post<Comment>(documentaryPaths.AddComment, {
      quranHubUserId: quranHubUserId,
      VideoInfoId: VideoInfoId,
      text: comment,
      verseId: verseId
    });
  }

  removeComment(CommentId: number): Observable<any> {
    let httpParams = new HttpParams().set('CommentId', CommentId);
    let options = { params: httpParams };

    return this.http.delete(documentaryPaths.RemoveComment, options);
  }

  addCommentReact(type: number, CommentId: number, quranHubUserId: string): Observable<React> {
    return this.http.post<React>(documentaryPaths.AddCommentReact, {
      Type: type,
      CommentId: CommentId,
      quranHubUserId: quranHubUserId,
    });
  }

  removeCommentReact(CommentId: number): Observable<any> {
    let httpParams = new HttpParams().set('CommentId', CommentId);
    let options = { params: httpParams };

    return this.http.delete(documentaryPaths.RemoveCommentReact, options);
  }
}
