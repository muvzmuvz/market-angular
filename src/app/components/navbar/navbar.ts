
import {ChangeDetectionStrategy, Component} from '@angular/core';
import {RouterLink, RouterLinkActive} from '@angular/router';
import {TuiTabBar} from '@taiga-ui/addon-mobile';
import {TuiTextfield} from '@taiga-ui/core';




@Component({
  selector: 'app-navbar',
  imports: [RouterLink, RouterLinkActive, TuiTabBar, TuiTextfield],
  templateUrl: './navbar.html',
  styleUrl: './navbar.less',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class Navbar {
  searchQuery: string = '';

 protected readonly routes: any = {
   'Главная': '/',
   'Профиль': '/profile',
   'Настройки': '/settings'
 };

}
