import { UserBasicInfo } from "./user/userBasicInfo.model";

export class Notification {
  constructor(
    public notificationId: number,
    public dateTime: string,
    public sourceUser: UserBasicInfo,
    public targetUser: UserBasicInfo,
    public message: string,
    public seen: boolean,
    public type: string,
    public postId: number,
    public postReactId: number,
    public commentId: number) {
  }
}
