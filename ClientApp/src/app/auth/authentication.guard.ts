import { Injectable } from "@angular/core";
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot }
    from "@angular/router";
import { skipWhile } from "rxjs/operators";
import { StateService } from "../stateService.service";
import { AuthenticationService } from "./authentication.service";

@Injectable()
export class AuthenticationGuard {
    authenticated: boolean = false;
    constructor(private router: Router,
                private authService: AuthenticationService, private stateService: StateService) {
                    this.stateService.pipe(skipWhile(newState => newState["authenticated"] == this.authenticated))
                    .subscribe(newState =>{
                      this.authenticated = newState["authenticated"];
                    });
                }

    canActivate(route: ActivatedRouteSnapshot,
            state: RouterStateSnapshot): boolean {
        if (this.authenticated) {
            return true;
        } else {
            this.authService.callbackURL = route.url.toString();
            this.router.navigateByUrl("/login");
            return false;
        }
    }
}
