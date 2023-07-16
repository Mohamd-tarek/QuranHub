import { Observable} from "rxjs";
import { Post } from "../../models/post/post.model";
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";

export abstract class HomeService {

  abstract getNewFeeds() : Observable<Post[]>; 
  
  abstract addPost(verseId: number, quranHubUserId: string, text:string, privacy: string): Observable<Post>;

  abstract findUsersByName(name: string): Observable<UserBasicInfo[]>;

  abstract searchPosts(keyword: string): Observable<Post[]>;
}
