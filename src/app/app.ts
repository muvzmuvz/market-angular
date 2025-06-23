import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { provideRouter } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
  ],
  templateUrl: './app.html',
  styleUrl: './app.less',

})
export class App {
  constructor(
    private oidcSecurityService: OidcSecurityService,
    private router: Router
  ) { }

  ngOnInit(): void {
    // Проверяем, есть ли параметры авторизации в URL
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