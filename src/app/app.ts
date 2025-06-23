import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.less'], // исправил на styleUrls
})
export class App {
  constructor(
    private oidcSecurityService: OidcSecurityService,
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object  // внедряем платформу
  ) {}

  ngOnInit(): void {
    // Проверяем, что код запускается в браузере
    if (!isPlatformBrowser(this.platformId)) {
      // Если не браузер — ничего не делаем
      return;
    }

    // Безопасно используем window
    const url = new URL(window.location.href);
    const hasAuthParams = url.searchParams.has('code') && url.searchParams.has('state');

    if (hasAuthParams) {
      this.oidcSecurityService.checkAuth().subscribe(({ isAuthenticated }) => {
        if (isAuthenticated) {
          // Убираем параметры из URL, оставляя только чистый путь
          this.router.navigateByUrl(window.location.pathname, { replaceUrl: true });
        } else {
          console.error('Пользователь не аутентифицирован');
        }
      });
    }
  }
}
