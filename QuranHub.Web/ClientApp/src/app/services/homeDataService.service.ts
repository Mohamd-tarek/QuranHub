import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { HomeService } from "../abstractions/services/homeService";
import { homePaths } from "../constants/home.constants";
import { Post } from "../models/post/post.model";
import { UserBasicInfo } from "../models/user/userBasicInfo.model"

@Injectable({
  providedIn: 'root',
})

export class HomeDataService extends HomeService {

  constructor(private http: HttpClient) {
    super();
  }

  getNewFeeds(): Observable<Post[]> {
    return this.http.get<Post[]>(homePaths.NewFeeds);
  }


  addPost(verseId: number, quranHubUserId: string, text: string, privacy: string): Observable<Post> {
    return this.http.post<Post>(homePaths.AddPost, {
      verseId: verseId,
      quranHubUserId: quranHubUserId,
      text: text,
      privacy: Number(privacy)
    });
  }

  findUsersByName(name: string): Observable<UserBasicInfo[]> {
    return this.http.get<UserBasicInfo[]>(homePaths.FindUsersByName + name);

  }

  searchPosts(keyword: string): Observable<Post[]> {
    return this.http.get<Post[]>(homePaths.SearchPosts + keyword);
  }

}
