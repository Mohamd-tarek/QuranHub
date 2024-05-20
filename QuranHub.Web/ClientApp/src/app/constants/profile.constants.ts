import { RestApiUrl } from "./application.constants";

export const profileURL = RestApiUrl + "/profile";

   interface ProfilePaths  {
      readonly UserPosts : string;
      readonly UserFollowers: string;
      readonly UserFollowings: string;
      readonly GetUserProfile: string;
      readonly GetCoverPicture: string,
      readonly EditCoverPicture: string,
      readonly GetProfilePicture: string,
      readonly EditProfilePicture: string,
      readonly CheckFollowing: string;
      readonly FollowUser: string;
      readonly UnfollowUser: string;
      readonly AboutInfo: string,
   }

export const profilePaths: ProfilePaths = {
   UserPosts : `${profileURL}/user-posts/`,
   UserFollowers : `${ profileURL }/user-followers/`,
   UserFollowings :`${profileURL}/user-followings/`,
   GetUserProfile:`${profileURL}/user-profile/`,
   GetCoverPicture:`${profileURL}/cover-picture/`,
   EditCoverPicture:`${profileURL}/edit-cover-picture/`,
   GetProfilePicture:`${profileURL}/profile-picture/`,
   EditProfilePicture:`${profileURL}/edit-profile-picture/`,
   CheckFollowing:`${profileURL}/check-following/`,
   FollowUser:`${profileURL}/follow-user/`,
   UnfollowUser:`${profileURL}/unfollow-user/`,
   AboutInfo:`${profileURL}/about-info/`,
}
