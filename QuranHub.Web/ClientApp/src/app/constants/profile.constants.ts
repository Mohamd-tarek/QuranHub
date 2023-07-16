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
   UserPosts : `${profileURL}/UserPosts/`,
   UserFollowers : `${ profileURL }/UserFollowers/`,
   UserFollowings :`${profileURL}/UserFollowings/`,
   GetUserProfile:`${profileURL}/UserProfile/`,
   GetCoverPicture:`${profileURL}/CoverPicture/`,
   EditCoverPicture:`${profileURL}/editCoverPicture/`,
   GetProfilePicture:`${profileURL}/ProfilePicture/`,
   EditProfilePicture:`${profileURL}/EditProfilePicture/`,
   CheckFollowing:`${profileURL}/CheckFollowing/`,
   FollowUser:`${profileURL}/FollowUser/`,
   UnfollowUser:`${profileURL}/UnFollowUser/`,
   AboutInfo:`${profileURL}/aboutInfo/`,
}
