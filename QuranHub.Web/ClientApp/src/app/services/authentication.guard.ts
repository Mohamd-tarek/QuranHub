import { Injectable } from "@angular/core";
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot }   from "@angular/router";
import { AuthenticationService } from "../abstractions/services/authenticationService";

@Injectable()
export class AuthenticationGuard {
    constructor(private router: Router, private authService: AuthenticationService) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (this.authService.isAuthenticated) {
            return true;
        } else {
            let url = this.getUrl(route);
            this.authService.callbackURL = url;
            this.router.navigateByUrl("auth/login");
            return false;
        }
    }

    getUrl(route: ActivatedRouteSnapshot): string {
        let url = route.url.toString();
        
            while (route.firstChild) {
                url += '/' +  route.firstChild.url.toString();
                route = route.firstChild;
            }

        return url;
    }
}
