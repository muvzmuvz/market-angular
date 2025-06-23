import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
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

  canActivate(): Observable<boolean> {
    // Проверяем, что код выполняется в браузере (не на сервере SSR)
    if (!isPlatformBrowser(this.platformId)) {
      // Для SSR просто разрешаем активацию, чтобы рендер не ломался
      return of(true);
    }

    // В браузере проверяем, аутентифицирован ли пользователь
    return this.oidcSecurityService.checkAuth().pipe(
      tap(({ isAuthenticated }) => {
        if (!isAuthenticated) {
          // Если не аутентифицирован — инициируем авторизацию
          // Через роутер для корректного Angular-навигейшена
          this.router.navigate(['/']);
          this.oidcSecurityService.authorize();
        }
      }),
      map(({ isAuthenticated }) => isAuthenticated),
      catchError((error) => {
        // В случае ошибки логируем и блокируем доступ
        console.error('Ошибка в AuthGuard:', error);
        return of(false);
      })
    );
  }
}
