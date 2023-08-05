import { Observable } from "rxjs";
import { UserBasicInfo } from "../../models/user/userBasicInfo.model";
import { PrivacySetting } from "../../models/user/privacySetting.model";

export enum Gender{
  Male = "0",
  Female = "1",
}

export enum Religion{
  Muslim  = "0" ,
  Christian = "1",
  Jewish = "2",
  Other = "3"
}

export interface IAboutInfo {
  dateOfBirth: string,
  gender: Gender;
  religion: Religion;
  aboutMe: string; 
}

   
export abstract class UserService{

  email!: string;
  password: string = "";
  callbackURL: string = "/";

  abstract getUser(): UserBasicInfo |null; 

  abstract getUserInfo(): void;

  abstract deleteUserInfo(): void;

  abstract getAboutInfo() :Observable<IAboutInfo>;

  abstract updateUser(user: UserBasicInfo) :void;

  abstract editUserData(email: string, username: string): Observable<any>;

  abstract getPrivacySetting(): Observable<PrivacySetting>;

  abstract editPrivacySetting(privacySetting: PrivacySetting): Observable<any>;

  abstract editAboutData(dateOfBirth:string, gender:string, religion: string, aboutMe: string): Observable<any>;

  abstract changePassword(current:string, newPassword:string, confirmPassword:string) :Observable<boolean>;
         
  abstract deleteAccount():void;

}
