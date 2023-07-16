import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Observable } from "rxjs";
import { profilePaths } from "../constants/profile.constants";
import { Post } from "../models/post/post.model";
import { ProfileService } from "../abstractions/services/profileService";
import { IAboutInfo } from "../abstractions/services/userService";
import { UserBasicInfo } from "../models/user/userBasicInfo.model";
import { Profile } from "../models/user/profile.model"

@Injectable({
  providedIn: 'root',
})    

export class ProfileDataService extends ProfileService { 

    constructor(private http: HttpClient) {
      super();
    }  

    getUserPosts(userId: string) :Observable<Post[]>{
      return this.http.get<Post[]>(profilePaths.UserPosts + userId ); 
    }

    getUserFollowers(userId: string) :Observable<UserBasicInfo[]>{
      return this.http.get<UserBasicInfo[]>(profilePaths.UserFollowers + userId ); 
    }

    getUserFollowersByKeyWord(userId: string, keyWord:string) :Observable<UserBasicInfo[]>{
      return this.http.get<UserBasicInfo[]>(profilePaths.UserFollowers + userId + '/' + keyWord ); 
    }

    getUserFollowings(userId: string) :Observable<UserBasicInfo[]>{
      return this.http.get<UserBasicInfo[]>(profilePaths.UserFollowings + userId ); 
    }


    getUserFollowingsByKeyWord(userId: string, keyWord:string) :Observable<UserBasicInfo[]>{
      return this.http.get<UserBasicInfo[]>(profilePaths.UserFollowings + userId + '/' + keyWord ); 
    }

    getUserProfile(userId: string): Observable<Profile> {
      return this.http.get<Profile>(profilePaths.GetUserProfile + userId ); 
    }

    editCoverPicture(formData: FormData): Observable<any[]> {
      return this.http.post<any>(profilePaths.EditCoverPicture, 
        formData);
    }

    editProfilePicture(formData: FormData): Observable<any[]> {
      return this.http.post<any>(profilePaths.EditProfilePicture,
        formData);
    }
  
    checkFollowed(userId :string): Observable<boolean> {
      return this.http.get<boolean>(profilePaths.CheckFollowing + userId ); 
    }
  
    followUser(followerId: string, followedId: string): Observable<boolean> {
      return   this.http.post<boolean>(profilePaths.FollowUser, {
        followerId: followerId,
        followedId: followedId,
      });
    }
  
    unfollowUser(followerId: string, followedId: string): Observable<boolean> {
      return   this.http.post<boolean>(profilePaths.UnfollowUser, {
        followerId: followerId,
        followedId: followedId,
      });
    }

    getAboutInfo(userId: string): Observable<IAboutInfo> {
      return this.http.get<IAboutInfo>(profilePaths.AboutInfo + userId);  
    }
}
