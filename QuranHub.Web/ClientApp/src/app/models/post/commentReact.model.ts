import { UserBasicInfo } from "../user/userBasicInfo.model";

export class CommentReact {
    constructor(
        public commentReactId : number,
        public dateTime : string ,
        public type : number ,
        public quranHubUser: UserBasicInfo ){ } 
}
