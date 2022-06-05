import { Injectable } from "@angular/core";
import { Repository } from "../models/repository";
import { Observable, of } from "rxjs";
import { map, catchError } from "rxjs/operators";
import { Router } from "@angular/router";
import { StateService } from "../stateService.service";
import { State } from "../models/state";

@Injectable()
export class AuthenticationService{
 
  
  constructor(private repo : Repository, private stateService: StateService,  private router : Router){
    this.state = stateService.getValue();
   }
  state : State;
  name!: string;
  password: string = "";
  callbackURL: string = "/";

  login(): Observable<boolean>{
    this.state.authenticated = false;
    return this.repo.login(this.name, this.password).pipe(
        map(response => {
          if(response){
            this.state.authenticated = true;
            this.password = "";
            this.router.navigateByUrl(this.callbackURL);
          }
          this.stateService.next(this.state);
          return this.state.authenticated;
        }),
        catchError(e => {
          this.state.authenticated = false;
          this.stateService.next(this.state);
          return of(false);
        }));
  }
  logout(){
    this.state.authenticated = false;
    this.stateService.next(this.state);
    this.repo.logout();
    this.router.navigateByUrl("/");
  }

}
