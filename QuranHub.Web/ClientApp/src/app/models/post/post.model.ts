import { PostUser } from "../user/postUser.model";
import { Quran } from "../quran/quran.model";
import { PostReact } from "./postReact.model";
import { Share } from "./share.model";
import { Comment } from "./comment.model";

export enum Privacy{
  Public = "0",
  Friends_Only = "1",
  Only_Me = "2"
}

export class Post {
    constructor(
      public postId : number,
      public privacy: Privacy,
      public dateTime: string ,
      public quranHubUser: PostUser ,
      public reactedTo: boolean,
      public verse : Quran ,
      public text :string , 
      public reactsCount: number,
      public commentsCount: number,
      public sharesCount: number,
      public share: Share,
      public reacts : PostReact [] ,
      public comments: Comment[] ,
      public shares : Share [] ) { }
  }
