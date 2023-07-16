import { RestApiUrl } from "./application.constants";

export const homeURL = RestApiUrl + "/Home";

interface HomePathsType  {
      readonly NewFeeds : string;
      readonly AddPost: string;
      readonly FindUsersByName: string;
      readonly SearchPosts: string;
}

export const homePaths: HomePathsType = {
   NewFeeds: `${homeURL}/NewFeeds/`,
   AddPost: `${ homeURL }/AddPost/`,
   FindUsersByName: `${homeURL}/FindUsersByName/`,
   SearchPosts: `${homeURL}/SearchPosts/`
}

