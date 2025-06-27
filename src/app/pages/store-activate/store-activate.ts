import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TuiButton } from '@taiga-ui/core';
import { ShopService, Shop, User } from 'src/app/service/shop-service/shop-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-store-activate',
  templateUrl: './store-activate.html',
  styleUrl: './store-activate.less',
  standalone: true,
  imports: [ReactiveFormsModule, CommonModule, TuiButton]
})
export class StoreActivate implements OnInit {
  step = 0;
  storeForm: FormGroup;
  message = '';
  currentUserId = '';

  constructor(
    private fb: FormBuilder,
    private shopService: ShopService,
    private router: Router
  ) {
    // Инициализируем форму с валидацией
    this.storeForm = this.fb.group({
      storeName: [''],
      storeAddress: [''],
      storePhone: [''],
      ownerName: [''],
      ownerEmail: [''],
      ownerPhone: ['']
    });
  }

  ngOnInit(): void {
    // Получаем текущего пользователя
    this.shopService.getMe().subscribe({
      next: (user) => {
        if (user && user.id) {
          this.currentUserId = user.id;
        } else {
          this.message = 'Пользователь не найден или не авторизован';
        }
      },
      error: () => {
        this.message = 'Ошибка при получении данных пользователя';
      }
    });
  }

  nextStep() {
    if (this.step === 0) {
      this.step = 1;
    } else if (this.step === 1 && this.storeForm.valid) {
      this.step = 2;
    } else if (this.step === 1) {
      this.storeForm.markAllAsTouched();
    }
  }

  prevStep() {
    if (this.step > 0) {
      this.step--;
    }
  }

  goToStore() {
    this.router.navigate(['/profile']);
  }

  onSubmit() {
    if (this.storeForm.valid && this.currentUserId) {
      const form = this.storeForm.value;

      const newShop: Shop = {
        name: form.storeName,
        description: form.storeAddress,
        userId: this.currentUserId
      };

      this.shopService.createShop(newShop).subscribe({
        next: () => {
          this.message = 'Магазин успешно создан!';
          this.step = 3;
        },
        error: () => {
          this.message = 'Ошибка при создании магазина';
        }
      });
    } else {
      this.storeForm.markAllAsTouched();
      this.message = 'Заполните все обязательные поля';
    }
  }
}
