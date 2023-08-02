import { React } from "../post/react.model";
import { Comment } from "../post/comment.model";


export class VideoInfo {
  constructor(
    public videoInfoId: number,
    public thumbnailImage: any,
    public name: string,
    public type: string,
    public size: number,
    public duration: number,
    public width: string,
    public height: string,
    public path: number,
    public views: number,
    public reactedTo: boolean,
    public reactsCount: number,
    public commentsCount: number,
    public reacts: React[],
    public comments: Comment[],
    ) { }  
  }
