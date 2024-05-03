import { Injectable } from "@angular/core";
import { Observable, of } from "rxjs";
import { map, catchError} from 'rxjs/operators';
import { Router} from "@angular/router";
import { HttpClient } from "@angular/common/http";
import { AuthenticationService, IExternalScheme } from "../abstractions/services/authenticationService";
import { identityPaths } from "../constants/authentication.constants";
import { SessionStorageService } from "./session-storage.service";
import { JWTTokenService } from "./jwt-token-service";

@Injectable({
  providedIn: 'root',
})

export class BasicAuthenticationService extends AuthenticationService {

  constructor( 
    private http: HttpClient,
    private router : Router,
    private session: SessionStorageService,
    private jwtService: JWTTokenService) {
    super();
  }

  email!: string;
  password: string = "";
  callbackURL: string = "/";
  authenticated: boolean = false;
  authenticationToken!: string | undefined;

  get isAuthenticated(): boolean {
    if (this.authenticated || this.authenticationToken !== undefined) {
          return true;
    }

    if (this.tryGetTokenFromSession()) {
      return true;
    }

    return false;
  }

  set isAuthenticated(value : boolean) {
    this.authenticated = value;
  }

  getAuthenticationToken() :string {
    if (!this.authenticationToken) {
      return this.authenticationToken as string
    }
    this.tryGetTokenFromSession();

    return this.authenticationToken;
  }

  getExternalSchema() :Observable<IExternalScheme[]> {
    return this.http.get<IExternalScheme[]>(identityPaths.ExternalSchemas );
  }

  login(): Observable<any> {
    return this.http.post<any>(identityPaths.LoginWithPassword, 
        {email: this.email, password: this.password})
  }

  loginCallback(): void{
    this.router.navigateByUrl(this.callbackURL);
  }

  logout() {
    this.session.clear();
    this.authenticationToken = undefined;
    this.authenticated = false;
  }

  logoutCallback(): void {
    this.router.navigateByUrl("/");
  }

  tryGetTokenFromSession(): boolean {
    let token = this.session.get("token") as string;
    if (token !== null) {
      this.authenticationToken = token;
      this.authenticated = true;
      return true;
    }
    return false;
  }

  storeAuthenticationToken(token: string) {
    this.authenticationToken = token;
    this.jwtService.setToken(token);
    this.authenticated = true;
  }
    
  setCookie(name:string, value:string, days:number) {
    var expires = "";
    if (days) {
      var date = new Date();
      date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
      expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
  }

  getCookie(key: string) : string {
   let cookieValue =  document.cookie
      .split("; ")
      .find((row) => row.startsWith(key))
      ?.split("=")[1];

    return cookieValue as string;
  }

  deleteCookie(name:string, path:string, domain:string) {
    if (this.getCookie(name)) {
    document.cookie = name + "=" +
      ((path) ? ";path=" + path : "") +
      ((domain) ? ";domain=" + domain : "") +
      ";expires=Thu, 01 Jan 1970 00:00:01 GMT";
  }
}

  loginExternal(provider: string): Observable<any> {
    return this.http.post<any>(identityPaths.LoginWithExternalProvider + provider, {});
  }

  loginExternalCallback(): Observable<boolean> {
    return this.http.get<boolean>(identityPaths.LoginWithExternalProviderCallback);
  }
  
  signup(): Observable<boolean>{
    return this.http.post<boolean>(identityPaths.Signup,
      { email: this.email, password: this.password })
  }

  signupExternal(provider: string): Observable<any>{
    return this.http.post<any>(identityPaths.SignupWithExternalProvider + provider, {});
  }

  signupExternalCallback(): Observable<any>{
    return this.http.get<any>(identityPaths.SignupWithExternalProviderCallback);
  }

  signupConfirm(email: string, token: string): Observable<boolean>{
    return this.http.post<boolean>(identityPaths.SignupConfirm, 
        {email: email, token: token}).pipe(
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

  signupCallback(): void {
    this.router.navigateByUrl("/login");
  }

  signupResend(email: string): Observable<boolean>{
    return this.http.post<boolean>(identityPaths.SignupResend, 
        {email: email}).pipe(
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

  recoverPassword(email: string): Observable<boolean>{
    return this.http.post<boolean>(identityPaths.RecoverPassword, 
        {email: this.email}).pipe(
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

  recoverPasswordConfirm(email: string, token: string, newPassword:string, confirmPassword:string ): Observable<boolean>{
    return this.http.post<boolean>(identityPaths.SignupConfirm, 
        {email: email,
         token: token, 
         newPassword: newPassword,
         confirmPassword: confirmPassword}).pipe(
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
}
