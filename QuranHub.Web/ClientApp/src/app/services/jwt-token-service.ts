import { Injectable } from '@angular/core';
import jwt_decode from 'jwt-decode';
import { SessionStorageService } from './session-storage.service';

@Injectable()
export class JWTTokenService extends SessionStorageService {

    jwtToken!: string;
    decodedToken!: { [key: string]: string };

    constructor() {
      super();
    }

    setToken(token: string) {
      this.set("token",token);
      if (token) {
        this.jwtToken = token;
      }
    }

    removeToken() {
      this.remove("token");
      this.jwtToken = '';
    }

    decodeToken() {
      if (this.jwtToken && this.jwtToken != "undefined") {
        this.decodedToken = jwt_decode(this.jwtToken);
      }else if(this.get("token")){
        let currUser = this.get("token");
        if(currUser!=null&&currUser!="undefined"){
          this.decodedToken = jwt_decode(currUser);
        }
      }
    }

    getDecodeToken() {
      return jwt_decode(this.jwtToken);
    }

    getUser() {
      this.decodeToken();
      return this.decodedToken ? this.decodedToken.UserName : null;
    }

    getExpiryTime() {
      this.decodeToken();
      return this.decodedToken ? this.decodedToken.exp : null;
    }

    isTokenExpired(): boolean {
      const expiryTime: string|null = this.getExpiryTime();
      if (expiryTime) {
        return ((1000 * parseInt(expiryTime)) - (new Date()).getTime()) < 60000;
      } else {
        return false;
      }
    }
}
