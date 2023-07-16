import { UserBasicInfo } from "../user/userBasicInfo.model";


export class PostReact {
    constructor(
      public postReactId : number,
      public dateTime : string,
      public type : number,
      public quranHubUser: UserBasicInfo,
      ) { }
  }
