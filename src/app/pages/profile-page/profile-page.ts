import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  OnInit,
} from '@angular/core';
import { NgForOf, NgIf } from '@angular/common';
import { Navbar } from 'src/app/components/navbar/navbar';
import { TuiTiles } from '@taiga-ui/kit';
import { CommonModule } from '@angular/common';
import { TuiFallbackSrcPipe, TuiTitle } from '@taiga-ui/core';
import { TuiAvatar } from '@taiga-ui/kit';
import { OrderCard } from 'src/app/components/order-card/order-card';
import { FormsModule } from '@angular/forms';
import { ImageUploadService } from 'src/app/service/image-upload/image-upload';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-profile-page',
  imports: [
    Navbar,
    TuiTiles,
    CommonModule,
    TuiFallbackSrcPipe,
    TuiTitle,
    TuiAvatar,
    OrderCard,
    FormsModule,
  ],
  templateUrl: './profile-page.html',
  styleUrl: './profile-page.less',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ProfilePage implements OnInit {
  username = 'rick';
  protected avatar = '';

  isEditModalOpen = false;
  editUsername = this.username;
  editAvatar = this.avatar;
  role: string = '';
  hover = false;
  selectedFileName: string | null = null;
  constructor(
    private cdr: ChangeDetectorRef,
    private imageUploadService: ImageUploadService,
    public auth: AuthService
  ) { }


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
          image:
            'https://cdn.citilink.ru/HoA3UJomX8Fc1dXBCqPUBk4Ve9CCd4Iz30M8zjn23LY/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/b4d17f80-d0d6-4e4e-97a1-a94dcee5d51a.jpg',
        },
        {
          title: 'Сумка для ноутбука',
          description: 'Удобная сумка с отделениями.',
          price: 2000,
          image:
            'https://cdn.citilink.ru/HoA3UJomX8Fc1dXBCqPUBk4Ve9CCd4Iz30M8zjn23LY/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/b4d17f80-d0d6-4e4e-97a1-a94dcee5d51a.jpg',
        },
      ],
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
          image:
            'https://cdn.citilink.ru/HoA3UJomX8Fc1dXBCqPUBk4Ve9CCd4Iz30M8zjn23LY/resizing_type:fit/gravity:sm/width:400/height:400/plain/product-images/b4d17f80-d0d6-4e4e-97a1-a94dcee5d51a.jpg',
        },
      ],
    },
  ];

  protected recentOrders = this.orders.slice(-2).reverse();
  ngOnInit() {
    this.auth.fetchUserRole().subscribe((role) => {
      this.role = role;
      this.cdr.detectChanges(); // Обновляем DOM
    });
  }

  openEditModal() {
    this.isEditModalOpen = true;
    this.editUsername = this.username;
    this.editAvatar = this.avatar;
  }

  saveProfile() {
    this.username = this.editUsername;
    this.avatar = this.editAvatar;
    this.isEditModalOpen = false;
  }

  cancelEdit() {
    this.isEditModalOpen = false;
  }

  onFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) return;

    const file = input.files[0];

    const reader = new FileReader();
    reader.onload = () => {
      this.avatar = reader.result as string;
    };
    reader.readAsDataURL(file);
  }

  onEditFileSelected(event: Event): void {
    const input = event.target as HTMLInputElement;
    if (!input.files?.length) {
      this.selectedFileName = null;
      return;
    }

    const file = input.files[0];
    this.selectedFileName = file.name;

    const reader = new FileReader();
    reader.onload = () => {
      this.editAvatar = reader.result as string;
    };
    reader.readAsDataURL(file);
  }

  uploadAvatar(): void {
    if (!this.editAvatar) {
      alert('Сначала выберите изображение для загрузки');
      return;
    }

    this.imageUploadService.uploadImage(this.editAvatar).subscribe({
      next: () => {
        alert('Аватар успешно загружен на сервер!');
        // Можно обновить локальный аватар сразу
        this.avatar = this.editAvatar;
        this.isEditModalOpen = false;
        this.cdr.markForCheck(); // Обновляем отображение
      },
      error: (error) => {
        alert('Ошибка при загрузке аватара: ' + error.message);
      },
    });
  }
}
