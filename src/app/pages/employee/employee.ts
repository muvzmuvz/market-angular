import { KeyValuePipe, NgForOf, NgIf, CommonModule, isPlatformBrowser } from '@angular/common';
import { ChangeDetectionStrategy, Component, signal, Inject, PLATFORM_ID } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { tuiAsPortal, TuiRepeatTimes, TuiPortals } from '@taiga-ui/cdk';
import { TuiAxes, TuiBarChart } from '@taiga-ui/addon-charts';
import { tuiCeil } from '@taiga-ui/cdk';
import {
  TuiAppearance,
  TuiButton,
  TuiDataList,
  TuiDropdown,
  TuiDropdownService,
  TuiIcon,
  TuiLink,
  TuiTextfield,
  TuiTitle,
} from '@taiga-ui/core';
import {
  TuiAvatar,
  TuiBadge,
  TuiBadgeNotification,
  TuiBreadcrumbs,
  TuiChevron,
  TuiDataListDropdownManager,
  TuiFade,
  TuiSwitch,
} from '@taiga-ui/kit';
import { TuiCardLarge, TuiForm, TuiHeader, TuiNavigation } from '@taiga-ui/layout';
import { SiteConfigService, SiteConfig } from 'src/app/service/SiteConfigService/site-config-service';

const ICON = 'data:image/svg+xml,…'; // твой ICON

@Component({
  selector: 'app-store-page',
  standalone: true,
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './employee.html',
  styleUrls: ['./employee.less'],
  providers: [TuiDropdownService, tuiAsPortal(TuiDropdownService)],
  imports: [
    CommonModule, FormsModule, KeyValuePipe, NgForOf, NgIf,
    RouterLink, RouterLinkActive,
    TuiAppearance, TuiButton, TuiDataList, TuiDropdown,
    TuiIcon, TuiLink, TuiTextfield, TuiTitle, TuiNavigation,
    TuiAvatar, TuiBadge, TuiBadgeNotification, TuiBreadcrumbs, TuiChevron,
    TuiDataListDropdownManager, TuiFade, TuiSwitch,
    TuiCardLarge, TuiForm, TuiHeader,
    TuiAxes, TuiBarChart, TuiRepeatTimes,
  ],
})
export class Employee {
  expanded = signal(false);
  open = false;
  darkMode = false;
  siteName = '';
  isMobile = false;

  drawer = {
    CRM: [
      { name: 'Сотрудники', icon: ICON, route: '/employees' },
      { name: 'Товары', icon: ICON, route: '/products' },
      { name: 'Доставки', icon: ICON, route: '/deliveries' },
    ],
    Components: [
      { name: 'Button', icon: ICON, route: '/button' },
      { name: 'Input', icon: ICON, route: '/input' },
      { name: 'Tooltip', icon: ICON, route: '/tooltip' },
    ],
    Essentials: [
      { name: 'Getting started', icon: ICON, route: '/getting-started' },
      { name: 'Showcase', icon: ICON, route: '/showcase' },
      { name: 'Typography', icon: ICON, route: '/typography' },
    ],
  };

  breadcrumbs = ['Home', 'Angular', 'Repositories', 'Taiga UI'];
  value = [[3660, 8281, 1069, 9034], [3952, 3671, 3781, 5323]];
  labelsX = ['Jan', 'Feb', 'Mar', 'Apr'];
  labelsY = ['0', '10 000'];

  constructor(
    private siteConfigService: SiteConfigService,
    @Inject(PLATFORM_ID) private platformId: Object,
  ) { }

  ngOnInit() {
    this.siteConfigService.getConfig().subscribe({
      next: (config: SiteConfig) => (this.siteName = config.siteName),
      error: () => (this.siteName = ''),
    });
    if (isPlatformBrowser(this.platformId)) {
      this.isMobile = window.innerWidth <= 768;
    }
  }

  toggleExpand() {
    this.expanded.update(v => !v);
  }

  getHeight(max: number) {
    return (max / tuiCeil(max, -3)) * 100;
  }
}
