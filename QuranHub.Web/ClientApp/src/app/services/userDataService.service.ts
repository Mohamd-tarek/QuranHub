import { Injectable } from "@angular/core";
import { Observable, of, BehaviorSubject } from "rxjs";
import { map, catchError} from 'rxjs/operators';
import { Router} from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { UserService, Gender, Religion, IAboutInfo } from "../abstractions/services/userService";
import { UserBasicInfo } from "../models/user/userBasicInfo.model";
import { userPaths } from "../constants/user.constants";
import { PrivacySetting } from "../models/user/privacySetting.model";

@Injectable({
  providedIn: 'root',
})

export class UserDataService extends UserService {

  constructor( private http: HttpClient, private router : Router) {
    super();
  }

  private userSubject: BehaviorSubject<UserBasicInfo | null> = new BehaviorSubject<UserBasicInfo | null>(null);

  getUser(): UserBasicInfo | null {
    if (this.userSubject.getValue() != null) {
      return this.userSubject.getValue();
    }

    let user = this.getUserFromStorage();

    if (user != null) {
      this.userSubject.next(user);
    }

    return this.userSubject.getValue();
  }

  public getUserInfo(){
    this.http.get<UserBasicInfo>(userPaths.UserInfo).subscribe(response => {
       this.storeUserInStorage(response);
       this.userSubject.next(response);
    });
  }

  public deleteUserInfo() {
    this.deleteUserFromStorage();
    this.userSubject.next(null);
  }

  getAboutInfo(): Observable<IAboutInfo> {
    return this.http.get<IAboutInfo>(userPaths.AboutInfo);  
  }

  updateUser(user: UserBasicInfo): void {
    this.storeUserInStorage(user);
    this.userSubject.next(user);
  }

  editUserData(email:string, username:string): Observable<any> {
    return this.http.post<any>(userPaths.EditUserInfo, 
        {email: email, username: username});
  }

  editAboutData(dateOfBirth: string, gender: string, religion: string, aboutMe: string): Observable<any> {
    return this.http.post<any>(userPaths.EditAboutInfo, 
      {dateOfBirth: dateOfBirth,
        gender: Number(gender),
        religion : Number(religion),
        aboutMe: aboutMe});
  }

  getPrivacySetting(): Observable<PrivacySetting> {
    return this.http.get<PrivacySetting>(userPaths.PrivacySetting);
  }

  editPrivacySetting(privacySetting: PrivacySetting): Observable<any> {
    return this.http.post<any>(userPaths.PrivacySetting,
      {
        allowFollow: privacySetting.allowFollow,
        allowComment: privacySetting.allowComment,
        allowShare: privacySetting.allowShare,
        appearInSearch: privacySetting.appearInSearch
      });
  }

  deleteAccount() {
    this.http.post<boolean>(userPaths.DeleteAccount, null).subscribe(response => { });
    this.router.navigateByUrl("/");
  }

  changePassword(current:string, newPassword:string, confirmPassword:string) :Observable<boolean>{
   return this.http.post<boolean>(userPaths.ChangePassword, 
        {Current: current, NewPassword: newPassword, ConfirmPassword: confirmPassword}).pipe(
        map(response => {
          if (response) {
            return true;
          }
          return false;
        }),
        catchError(e => {
          return of(false);
        }));
  }

  private getUserFromStorage(): UserBasicInfo | null {
    let user: UserBasicInfo |null  = null;  
    if(localStorage.getItem("userName") != null){
      let id :string = localStorage.getItem("id") as string;
      let username :string = localStorage.getItem("userName") as string;
      let email :string = localStorage.getItem("email") as string;
      let profilePicture = localStorage.getItem("profilePicture");
      user = {id : id,
              userName : username,
              email : email,
              profilePicture : profilePicture};
    }
    return user ;
  }

  private storeUserInStorage(user: UserBasicInfo) {

    for (const [key, value] of Object.entries(user)){
      localStorage.setItem(key.toString(), value);
    }
  }

  private deleteUserFromStorage() {
    let user = this.getUser() as UserBasicInfo; 
    for (const key of Object.keys(user)){
      localStorage.removeItem(key.toString());
    }
  }
}
