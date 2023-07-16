import { UserBasicInfo } from "../user/userBasicInfo.model";
import { CommentReact } from "./commentReact.model";
import { Quran } from "../quran/quran.model";


export class Comment  {
    constructor(
        public commentId : number,
        public dateTime : string ,
        public quranHubUser: UserBasicInfo,
        public verse: Quran,
        public reactedTo: boolean,
        public reactsCount: number,
        public reacts : CommentReact [] ,
        public text : string 
    ){}
  
}
