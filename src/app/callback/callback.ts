// callback.component.ts
import { Component, OnInit } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-callback',
  template: `<p>Авторизация завершена. Перенаправляем...</p>`,
})
export class Callback implements OnInit {
  constructor(private oidcSecurityService: OidcSecurityService) {}

  ngOnInit(): void {
    this.oidcSecurityService.checkAuth().subscribe(({ isAuthenticated }) => {
      if (isAuthenticated) {
        // Перенаправляем на нужную страницу, например, главную
        window.location.href = '/';
      } else {
        console.error('Пользователь не аутентифицирован');
      }
    });
  }
}
