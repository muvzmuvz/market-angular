import { Component, Inject, PLATFORM_ID } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { isPlatformBrowser } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

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
    @Inject(PLATFORM_ID) private platformId: Object
  ) { }

  ngOnInit(): void {
    if (!isPlatformBrowser(this.platformId)) {
      return;
    }

    const url = new URL(window.location.href);
    const hasAuthParams = url.searchParams.has('code') && url.searchParams.has('state');

    if (hasAuthParams) {
      this.oidcSecurityService.checkAuth().subscribe(({ isAuthenticated }) => {
        if (isAuthenticated) {
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