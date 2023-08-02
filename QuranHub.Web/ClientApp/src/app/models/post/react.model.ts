import { UserBasicInfo } from "../user/userBasicInfo.model";


export class React {
    constructor(
      public ReactId : number,
      public dateTime : string,
      public type : number,
      public quranHubUser: UserBasicInfo,
      ) { }
  }
