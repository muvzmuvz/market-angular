import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { isPlatformBrowser } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { AuthInterceptor } from './auth/auth-intercreptor/auth-intercreptor';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
  ],
  templateUrl: './app.html',
  styleUrls: ['./app.less'], // исправил на styleUrls
  providers: [
        {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ]
})
export class App {
  constructor(
    private oidcSecurityService: OidcSecurityService,
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object
  ) { }

  ngOnInit(): void {
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    const url = new URL(window.location.href);
    const hasAuthParams = url.searchParams.has('code') && url.searchParams.has('state');

    if (hasAuthParams) {
      this.oidcSecurityService.checkAuth().subscribe(({ isAuthenticated, accessToken }) => {
        if (isAuthenticated) {
          document.cookie = `access_token=${accessToken}; path=/`;
          const redirectUrl = localStorage.getItem('redirectAfterLogin') || '/';
          localStorage.removeItem('redirectAfterLogin');
          this.router.navigateByUrl(redirectUrl, { replaceUrl: true });
        } else {
          console.error('Пользователь не аутентифицирован');
        }
      });
    }
  }
}
