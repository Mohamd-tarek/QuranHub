import { Injectable } from "@angular/core";
import { Repository } from "../models/repository";
import { Observable, of } from "rxjs";
import { map, catchError } from "rxjs/operators";
import { Router } from "@angular/router";

@Injectable()
export class AuthenticationService{
 
  
  constructor(private repo : Repository, private router : Router){ }
  
  authenticated: boolean = false;
  name!: string;
  password: string = "";
  callbackURL: string = "/";

  login(): Observable<boolean>{
    this.authenticated = false;
    return this.repo.login(this.name, this.password).pipe(
        map(response => {
          if(response){
            this.authenticated = true;
            this.password = "";
            this.router.navigateByUrl(this.callbackURL);
          }
          return this.authenticated;
        }),
        catchError(e => {
          this.authenticated = false;
          return of(false);
        }));
  }
  logout(){
    this.authenticated = false;
    this.repo.logout();
    this.router.navigateByUrl("/");
  }

}
