import { PlayListInfo } from "src/app/models/video/playListInfo.model";
import { Observable } from "rxjs";
import { VideoInfo } from "src/app/models/video/VideoInfo.model";
import { React } from "../../models/post/react.model";
import { CommentRepository } from "./CommentRepository";


export abstract class DocumentaryRepository extends CommentRepository {


  abstract getPlayLists(): Observable<PlayListInfo[]>;

  abstract getPlayListInfo(palylistName : string ): Observable<PlayListInfo>;

  abstract getVideoInfoForPlayList(playListName: string, offset: number, amount: number): Observable<VideoInfo[]>;

  abstract GetVideoInfoAsync(name: string): Observable<VideoInfo>;

  abstract getVideoInfoByIdWithSpecificComment(VideoInfoId: number, commentId: number): Observable<VideoInfo>;

  abstract loadMoreReacts(VideoInfoId: number, offset: number, size: number): Observable<React[]>;

  abstract addReact(type: number, VideoInfoId: number): Observable<React>;

  abstract removeReact(VideoInfoId: number): Observable<any>;
}
