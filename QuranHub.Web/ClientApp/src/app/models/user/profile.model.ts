import { User } from "./user.model";

export class Profile extends User {
  constructor(
    public id: string,
    public profilePicture: any,
    public email: string,
    public userName: string,
    public numberOfFollower: number,
    public numberOfFollowed: number,
    public coverPicture: any) {

    super(
       id,
       profilePicture,
       email,
       userName,
       numberOfFollower,
       numberOfFollowed);
  } 
}
