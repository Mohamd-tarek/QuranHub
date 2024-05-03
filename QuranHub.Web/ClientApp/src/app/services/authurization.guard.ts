import { Injectable } from "@angular/core";
import { Router, ActivatedRouteSnapshot, RouterStateSnapshot, ActivatedRoute }   from "@angular/router";
import { AuthenticationService } from "../abstractions/services/authenticationService";
import { UserPermissionService } from './user-permission.service';


@Injectable()
export class AuthurizationGuard {
  constructor(
    private router: Router,
    private authService: AuthenticationService,
    private userPermissionService: UserPermissionService) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot, route1: ActivatedRoute): boolean {
    let page = route.data.page;
    let permission = route.data.permission;
    let canAccess = this.userPermissionService.canAccess(page, permission);

        if (canAccess) {
            return true;
        }
        else {
          this.router.navigateByUrl("/");
           return false;
        }
    }
}
