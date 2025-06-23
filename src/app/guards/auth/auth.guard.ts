import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { Observable, of } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate {
  constructor(
    private oidcSecurityService: OidcSecurityService,
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    if (!isPlatformBrowser(this.platformId)) {
      return of(true);
    }

    return this.oidcSecurityService.checkAuth().pipe(
      tap(({ isAuthenticated }) => {
        if (!isAuthenticated) {
          localStorage.setItem('redirectAfterLogin', state.url); // Сохраняем URL, куда пользователь хотел попасть
          this.oidcSecurityService.authorize(); // Запускаем процесс авторизации
        }
      }),
      map(({ isAuthenticated }) => isAuthenticated),
      catchError((error) => {
        console.error('Ошибка в AuthGuard:', error);
        return of(false);
      })
    );
  }
}
