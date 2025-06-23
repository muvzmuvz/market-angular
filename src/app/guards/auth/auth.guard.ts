import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(private oidcSecurityService: OidcSecurityService, private router: Router) { }

  canActivate(): Observable<boolean> {
    return this.oidcSecurityService.checkAuth().pipe(
      map(({ isAuthenticated }) => {
        if (!isAuthenticated) {
          if (typeof window !== 'undefined') {
            this.oidcSecurityService.authorize();
          }
          return false;
        }
        return true;
      })
    );
  }
}
