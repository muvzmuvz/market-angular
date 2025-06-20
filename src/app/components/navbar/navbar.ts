import {
  ChangeDetectionStrategy,
  Component,
  OnInit,
  signal,
  effect
} from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { TuiTabBar } from '@taiga-ui/addon-mobile';
import { TuiTextfield } from '@taiga-ui/core';
import {
  SiteConfigService,
  SiteConfig
} from 'src/app/service/SiteConfigService/site-config-service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [RouterLink, RouterLinkActive, TuiTabBar, TuiTextfield],
  templateUrl: './navbar.html',
  styleUrl: './navbar.less',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Navbar implements OnInit {
  searchQuery: string = '';
  siteName: string ='';

  protected readonly routes: any = {
    'Главная': '/',
    'Профиль': '/profile',
    'Настройки': '/settings'
  };

  constructor(private siteConfigService: SiteConfigService) { }

  ngOnInit(): void {
    this.siteConfigService.getConfig().subscribe({
      next: (config: SiteConfig) => {
        this.siteName = config.siteName;
      },
      error: () => {
        this.siteName = ''; // дефолтное значение на случай ошибки
      }
    });
  }
}
