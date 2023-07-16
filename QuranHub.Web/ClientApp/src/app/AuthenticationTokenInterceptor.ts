import { HttpInterceptor, HttpXsrfTokenExtractor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { AuthenticationService } from "./abstractions/services/authenticationService";

@Injectable()

export class AuthenticationTokenInterceptor implements HttpInterceptor {

  constructor(private authenticationService: AuthenticationService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const headerName = 'Authorization';
    const value = this.authenticationService.getAuthenticationToken();
    req = req.clone({ headers: req.headers.set(headerName,`Bearer ${value}`) });
    
    return next.handle(req);
  }
}

