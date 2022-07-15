import { Injectable } from "@angular/core";
import { Repository } from "../models/repository";
import { Observable, of } from "rxjs";
import { map, catchError } from "rxjs/operators";
import { Router } from "@angular/router";
import { StateService } from "../stateService.service";
import { skipWhile } from "rxjs/operators";

@Injectable()
export class AuthenticationService{
 
  
  constructor(private repo : Repository, private stateService: StateService,  private router : Router){
    this.stateService.pipe(skipWhile(newState => newState["authenticated"] == this.authenticated))
    .subscribe(newState =>{
      this.authenticated = newState["authenticated"];
    });
  }

  authenticated: boolean = false;
  email!: string;
  password: string = "";
  callbackURL: string = "/";

  login(): Observable<boolean>{
    this.authenticated = false;
    return this.repo.login(this.email, this.password).pipe(
        map(response => {
          if(response){
            this.authenticated = true;
            this.password = "";
            this.router.navigateByUrl(this.callbackURL);
          }
          let state:any  = {"authenticated" : this.authenticated}
          this.stateService.next(state);
          return this.authenticated;
        }),
        catchError(e => {
          this.authenticated = false;
          let state:any  = {"authenticated": this.authenticated}
          this.stateService.next(state);
          return of(false);
        }));
  }
  logout(){
    this.authenticated = false;
    let state: any = {"authenticated": this.authenticated}
    this.stateService.next(state); 
    this.repo.logout();
    this.router.navigateByUrl("/");
  }

}
