import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Injectable({ providedIn: 'root' })
export class AuthService {
  constructor(private oidcService: OidcSecurityService) {}

  login() {
    this.oidcService.authorize();
  }

  logout() {
    this.oidcService.logoff();
  }

  get isAuthenticated$() {
    return this.oidcService.isAuthenticated$;
  }

  get userData$() {
    return this.oidcService.userData$;
  }
}