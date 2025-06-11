import { ChangeDetectionStrategy, Component } from '@angular/core';
import { NgForOf, NgIf } from '@angular/common';
import { Navbar } from 'src/app/components/navbar/navbar';
import { TuiIcon } from '@taiga-ui/core';
import { TuiTiles } from '@taiga-ui/kit';
import { CommonModule } from '@angular/common';
import { TuiFallbackSrcPipe, TuiIconPipe, TuiTitle } from '@taiga-ui/core';
import { TuiAvatar } from '@taiga-ui/kit';
import { title } from 'process';
import { OrderCard } from 'src/app/components/order-card/order-card';
const username = 'rick';
@Component({
  selector: 'app-profile-page',
  imports: [Navbar, TuiIcon, TuiTiles, CommonModule, TuiFallbackSrcPipe, TuiIconPipe, TuiTitle, TuiAvatar, OrderCard],
  templateUrl: './profile-page.html',
  styleUrl: './profile-page.less',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfilePage {
editProfile() {
throw new Error('Method not implemented.');
}
  username = 'rick';
  orders = [
    {
      order: 101,
      date: new Date('2024-12-01'),
      amount: 77000,
      items: [
        {
          title: 'Ноутбук Lenovo IdeaPad',
          description: 'Мощный ноутбук для работы и учебы с экраном 15.6".',
          price: 75000,
          image: 'https://cdn.citilink.ru/HoA3UJomX8Fc1dXBCqPUBk4Ve9CCd4Iz30M8zjn23LY/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/b4d17f80-d0d6-4e4e-97a1-a94dcee5d51a.jpg'
        },
        {
          title: 'Сумка для ноутбука',
          description: 'Удобная сумка с отделениями.',
          price: 2000,
          image: 'https://cdn.citilink.ru/HoA3UJomX8Fc1dXBCqPUBk4Ve9CCd4Iz30M8zjn23LY/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/b4d17f80-d0d6-4e4e-97a1-a94dcee5d51a.jpg'
        },
        {
          title: 'Сумка для ноутбука',
          description: 'Удобная сумка с отделениями.',
          price: 2000,
          image: 'https://cdn.citilink.ru/HoA3UJomX8Fc1dXBCqPUBk4Ve9CCd4Iz30M8zjn23LY/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/b4d17f80-d0d6-4e4e-97a1-a94dcee5d51a.jpg'
        }
      ]
    },
    {
      order: 102,
      date: new Date('2024-12-10'),
      amount: 12000,
      items: [
        {
          title: 'Мышь Logitech',
          description: 'Беспроводная мышь.',
          price: 12000,
          image: 'https://cdn.citilink.ru/HoA3UJomX8Fc1dXBCqPUBk4Ve9CCd4Iz30M8zjn23LY/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/b4d17f80-d0d6-4e4e-97a1-a94dcee5d51a.jpg'
        }
      ]
    }
  ];
  protected avatar = 'https://postila.ru/resize?w=460&src=%2Fs3%2Ff52%2F74%2F1fb62bf1d821948a49204406bdc.jpg';
  protected recentOrders = this.orders.slice(-2).reverse();
}
