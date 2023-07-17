import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { HttpClient} from "@angular/common/http";
import { DocumentaryRepository } from "src/app/abstractions/repositories/documentaryRepository";
import { documentaryPaths } from "../../constants/documentary.constants";
import { PlayListInfo } from "src/app/models/video/playListInfo.model";
import { VideoInfo } from "src/app/models/video/VideoInfo.model";



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
    
  getVideoInfoForPlayList(playListName: string, offset: number, amount: number): Observable<VideoInfo[]>
  {
    return this.http.get<VideoInfo[]>(documentaryPaths.VideoInfoForPlayList + '/' + playListName + '/' + offset + '/' + amount);
  }

  GetVideoInfoAsync(name: string): Observable<VideoInfo> {
    return this.http.get<VideoInfo>(documentaryPaths.VideoInfoForPlayList + '/' + name);
  }
}
