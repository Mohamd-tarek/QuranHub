import { RestApiUrl } from "./application.constants";

export const homeURL = RestApiUrl + "/home";

interface HomePathsType  {
      readonly NewFeeds : string;
      readonly AddPost: string;
      readonly FindUsersByName: string;
      readonly SearchPosts: string;
}

export const homePaths: HomePathsType = {
   NewFeeds: `${homeURL}/new-feeds/`,
   AddPost: `${ homeURL }/add-post/`,
   FindUsersByName: `${homeURL}/find-users-by-name/`,
   SearchPosts: `${homeURL}/search-posts/`
}

