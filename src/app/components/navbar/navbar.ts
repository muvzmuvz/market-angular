import { ChangeDetectionStrategy, Component, OnInit } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { TuiTabBar } from '@taiga-ui/addon-mobile';
import { TuiTextfield } from '@taiga-ui/core';
import { SiteConfigService, SiteConfig } from 'src/app/service/SiteConfigService/site-config-service';

import { OidcSecurityService } from 'angular-auth-oidc-client';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, TuiTabBar, TuiTextfield],
  templateUrl: './navbar.html',
  styleUrls: ['./navbar.less'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Navbar implements OnInit {
  searchQuery: string = '';
  siteName: string = '';

  isAuthenticated = false;

  protected readonly routes: any = {
    'Главная': '/',
    'Профиль': '/profile',
    'Настройки': '/settings',
  };

  constructor(
    private siteConfigService: SiteConfigService,
    private oidcSecurityService: OidcSecurityService
  ) {}

  ngOnInit(): void {
    this.siteConfigService.getConfig().subscribe({
      next: (config: SiteConfig) => {
        this.siteName = config.siteName;
      },
      error: () => {
        this.siteName = ''; // дефолтное значение на случай ошибки
      },
    });

    // Подписываемся на статус аутентификации
this.oidcSecurityService.isAuthenticated$.subscribe((authResult) => {
  this.isAuthenticated = authResult.isAuthenticated;
});

  }

  login() {
    this.oidcSecurityService.authorize();
  }

  logout() {
    this.oidcSecurityService.logoff();
  }
}
