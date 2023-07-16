import { Observable } from "rxjs";
import { Post } from "../../models/post/post.model";
import { Profile } from "../../models/user/profile.model";
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";
import { IAboutInfo } from "./userService";

export abstract  class ProfileService {

  abstract getUserProfile(userId: string): Observable<Profile>;
  
  abstract editCoverPicture(formData:FormData): Observable<any[]>;
  
  abstract editProfilePicture(formData: FormData): Observable<any[]>;

  abstract checkFollowed(userId: string) : Observable<boolean>; 

  abstract followUser(followerId: string, followedId: string) :Observable<boolean>;

  abstract unfollowUser(followerId: string, followedId: string) :Observable<boolean>;

  abstract getUserPosts(userId: string) :Observable<Post[]>;

  abstract getUserFollowers(userId: string): Observable<UserBasicInfo[]>;

  abstract getUserFollowersByKeyWord(userId: string, keyWord: string): Observable<UserBasicInfo[]>;

  abstract getUserFollowings(userId: string): Observable<UserBasicInfo[]>;

  abstract getUserFollowingsByKeyWord(userId: string, keyWord: string): Observable<UserBasicInfo[]>;

  abstract getAboutInfo(userId: string) :Observable<IAboutInfo>;
}
