import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { TuiButton } from '@taiga-ui/core';
import { ShopService, Shop, User } from 'src/app/service/shop-service/shop-service'
import { Route } from '@angular/router';
import { routes } from 'src/app/app.routes';
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
  users: User[] = [];
  message = '';

  constructor(
    private fb: FormBuilder,
    private shopService: ShopService,
    private router: Router
  ) {
    this.storeForm = this.fb.group({
      storeName: [''],
      storeAddress: [''],
      storePhone: [''],
      ownerName: [''],
      ownerEmail: [''],
      ownerPhone: [''],
      userId: [''] // добавим выбор пользователя
    });
  }

  ngOnInit(): void {
    // загружаем пользователей
    this.shopService.getUsers().subscribe({
      next: (data) => (this.users = data),
      error: () => (this.message = 'Ошибка при загрузке пользователей')
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
    if (this.storeForm.valid) {
      const form = this.storeForm.value;

      const newShop: Shop = {
        name: form.storeName,
        description: form.storeAddress,
        userId: form.userId
      };

      this.shopService.createShop(newShop).subscribe({
        next: () => {
          this.message = 'Магазин успешно создан!';
          this.step = 3; // например, шаг завершения
        },
        error: () => (this.message = 'Ошибка при создании магазина')
      });
    }
  }
}
