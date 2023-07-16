import { UserBasicInfo } from "../user/userBasicInfo.model";
import { Post } from "./post.model";

export class Share  {
    constructor(
        public shareId : number ,
        public post: Post,
        public dateTime : string,
        public quranHubUser: UserBasicInfo ) {}
}
