import { Observable } from "rxjs";

export interface IExternalScheme{
  name: string;
  displayName: string; 
}
   
export abstract class AuthenticationService{

  email!: string;
  password: string = "";
  callbackURL: string = "/";

  abstract get isAuthenticated(): boolean;

  abstract getExternalSchema(): Observable<IExternalScheme[]>;

  abstract getAuthenticationToken(): string;

  abstract login(): Observable<any>;

  abstract loginCallback(): void;

  abstract storeAuthenticationToken(token: string): void;

  abstract loginExternal(provider:string): Observable<any>;

  abstract loginExternalCallback(): Observable<any>;
  
  abstract logout(): void;

  abstract logoutCallback(): void;

  abstract signup(): Observable<boolean>;

  abstract signupCallback(): void;

  abstract signupExternal(provider:string): Observable<any>;

  abstract signupExternalCallback(): Observable<any>;

  abstract signupConfirm(email: string, token: string): Observable<boolean>;

  abstract signupResend(email: string): Observable<boolean> ;

  abstract recoverPassword(email: string): Observable<boolean>;
  
  abstract recoverPasswordConfirm(email: string, token: string, newPassword:string, confirmPassword:string ): Observable<boolean>;
         
}
