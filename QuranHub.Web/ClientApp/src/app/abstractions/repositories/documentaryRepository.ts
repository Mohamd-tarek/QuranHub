import { PlayListInfo } from "src/app/models/video/playListInfo.model";
import { Observable } from "rxjs";
import { VideoInfo } from "src/app/models/video/VideoInfo.model";

export abstract class DocumentaryRepository {


  abstract getPlayLists(): Observable<PlayListInfo[]>;

  abstract getVideoInfoForPlayList(playListName: string, offset: number, amount: number): Observable<VideoInfo[]>;

  abstract GetVideoInfoAsync(name :string ): Observable<VideoInfo>;

}
