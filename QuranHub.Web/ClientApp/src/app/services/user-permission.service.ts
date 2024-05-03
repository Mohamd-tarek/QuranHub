import { Injectable } from '@angular/core';
import { PageEnum, PermissionEnum, RolesTypesEnum } from '../models/enums';
import { JWTTokenService } from './jwt-token-service';


@Injectable({
  providedIn: 'root',
})
export class UserPermissionService {

  constructor(private jwtService: JWTTokenService) { }


  canAccess(page: PageEnum, permission: PermissionEnum): boolean {

    let pagePermissions = this.jwtService.decodedToken[page];
    if (pagePermissions !== undefined) {
      let permissions = pagePermissions.split(',');
      return permissions.findIndex((p: any) => p === permission) > -1;
    }
    else {
      return false;
    }

  }

  isInRole(role: RolesTypesEnum): boolean {
    let roles = this.jwtService.decodedToken["role"];
    console.log(this.jwtService.decodedToken);
    if (roles !== undefined) {
      let userRoles = roles.split(',');
      return userRoles.findIndex((p: any) => p === role) > -1;
    }
    else {
      return false;
    }

  }


}
