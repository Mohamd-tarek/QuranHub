import { UserBasicInfo } from "./userBasicInfo.model";

export class User extends UserBasicInfo {
  constructor(
    public id: string,
    public profilePicture: any,
    public email: string,
    public userName: string,
    public numberOfFollower: number,
    public numberOfFollowed: number) {

    super(id, profilePicture, email, userName);
  }
    
}
