import { InitStatusService } from './../service/init-status-service';
import { Injectable } from '@angular/core';
import { CanActivate, Router, UrlTree } from '@angular/router';
import { Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class StartupRedirectGuard implements CanActivate {

  constructor(
    private initStatusService: InitStatusService,
    private router: Router
  ) { }

  canActivate(): Observable<boolean | UrlTree> {
    const currentUrl = this.router.url;

    return this.initStatusService.isInitialized().pipe(
      map(isInit => {
        if (isInit) {
          // Если сайт инициализирован — пропускаем все маршруты без редиректа
          return true;
        } else {
          // Если сайт НЕ инициализирован, но мы уже на /install — пропускаем
          if (currentUrl === '/install') {
            return true;
          }
          // Иначе редиректим на /install
          return this.router.createUrlTree(['/install']);
        }
      }),
      catchError(() => {
        // При ошибке — редирект на /install
        if (currentUrl === '/install') {
          return of(true);
        }
        return of(this.router.createUrlTree(['/install']));
      })
    );
  }
}
