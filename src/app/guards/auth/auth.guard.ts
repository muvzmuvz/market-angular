import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private oidcSecurityService: OidcSecurityService,
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object
  ) { }

  canActivate(): Observable<boolean> {
    // ✅ Проверка, в браузере ли мы
    if (!isPlatformBrowser(this.platformId)) {
      // ⛔ SSR — возвращаем true, чтобы не ломать рендер
      return of(true);
    }

    // ✅ Только в браузере вызываем checkAuth()
    return this.oidcSecurityService.checkAuth().pipe(
      map(({ isAuthenticated }) => {
        if (!isAuthenticated) {
          this.oidcSecurityService.authorize();
          return false;
        }
        return true;
      })
    );
  }
}
