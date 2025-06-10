import { ChangeDetectionStrategy, Component } from '@angular/core';
import {NgForOf, NgIf} from '@angular/common';
import { Navbar } from 'src/app/components/navbar/navbar';
import { TuiIcon } from '@taiga-ui/core';
import { TuiTiles } from '@taiga-ui/kit';
import { CommonModule } from '@angular/common';
	import {TuiFallbackSrcPipe, TuiIconPipe, TuiTitle} from '@taiga-ui/core';
import {TuiAvatar} from '@taiga-ui/kit';
import { title } from 'process';
@Component({
  selector: 'app-profile-page',
  imports: [Navbar, TuiIcon, TuiTiles, CommonModule, TuiFallbackSrcPipe, TuiIconPipe, TuiTitle, TuiAvatar],
  templateUrl: './profile-page.html',
  styleUrl: './profile-page.less',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfilePage {
  protected usename = 'rick';
  protected order = new Map();
  protected avatar = 'https://postila.ru/resize?w=460&src=%2Fs3%2Ff52%2F74%2F1fb62bf1d821948a49204406bdc.jpg';
  protected items = [
    { w: 1, h: 1, content: 'Item 1' },
    { w: 1, h: 1, content: 'Item 2' },
    { w: 2, h: 1, content: 'Item 3' },
    { w: 1, h: 1, content: 'Item 4' },
    { w: 3, h: 1, content: 'Item 5' },
    { w: 1, h: 1, content: 'Item 6' },
    { w: 1, h: 1, content: 'rick' },
    { w: 1, h: 1, content: 'Item 8' },
    { w: 1, h: 1, content: 'Item 9' },
  ];

  protected orders = [
    { title: 'Item 1', order: 0, description: 'Description for Item 1', date: new Date('2023-10-01'), amount: 100, image: 'https://postila.ru/resize?w=460&src=%2Fs3%2Ff52%2F74%2F1fb62bf1d821948a49204406bdc.jpg' },
    { title: 'Item 2', order: 1, description: 'Description for Item 2', date: new Date('2023-10-02'), amount: 200, image: 'https://postila.ru/resize?w=460&src=%2Fs3%2Ff52%2F74%2F1fb62bf1d821948a49204406bdc.jpg' },
    { title: 'Item 2', order: 1, description: 'Description for Item 2', date: new Date('2023-10-02'), amount: 200 },
    { title: 'Item 2', order: 1 },
    { title: 'Item 3', order: 2 },
    { title: 'Item 4', order: 3 },
    { title: 'Item 5', order: 4 },
    { title: 'Item 6', order: 5 },
    { title: 'rick', order: 6 },
    { title: 'Item 8', order: 7 },
    { title: 'Item 9', order: 8 },
  ];
}
