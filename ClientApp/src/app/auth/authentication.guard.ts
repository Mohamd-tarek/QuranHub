import { Injectable } from "@angular/core";
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot }
    from "@angular/router";
import { skipWhile } from "rxjs/operators";
import { State } from "../models/state";
import { StateService } from "../stateService.service";
import { AuthenticationService } from "./authentication.service";

@Injectable()
export class AuthenticationGuard {
    state: State;
    constructor(private router: Router,
                private authService: AuthenticationService, private stateService: StateService) {
                    this.state = stateService.getValue();
                    this.stateService.pipe(skipWhile(newState => newState.authenticated != this.state.authenticated))
                    .subscribe(state =>{
                      this.state = state;
                    });
                }

    canActivate(route: ActivatedRouteSnapshot,
            state: RouterStateSnapshot): boolean {
        if (this.state.authenticated) {
            return true;
        } else {
            this.authService.callbackURL = route.url.toString();
            this.router.navigateByUrl("/login");
            return false;
        }
    }
}
