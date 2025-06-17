import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Navbar } from 'src/app/components/navbar/navbar';

@Component({
  selector: 'app-cart-page',
  imports: [
    CommonModule,
    FormsModule,
    Navbar
  ],
  templateUrl: './cart-page.html',
  styleUrls: ['./cart-page.less'],
  standalone: true,
})
export class CartPage {
  step = 1; // текущий шаг

  cartItems = [
    {
      name: 'Товар 1',
      price: 1000,
      quantity: 2,
      image:
        'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' // пример картинки
    },
    {
      name: 'Товар 2',
      price: 500,
      quantity: 1,
      image:
        'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg'
    }
  ];
  delivery = { name: '', phone: '', address: '', postCode: '' };  
  paymentMethod: string = '';
  card = { number: '', expiry: '', cvv: '' };

  submitOrder() {
    this.step = 2;
  }

  submitDelivery() {
    if (this.delivery.name && this.delivery.phone && this.delivery.address && this.delivery.postCode) {
      this.step = 3;
    }
  }

  submitPayment() {
    // Обработка оплаты
    alert('Оплата прошла успешно!');
    this.step = 1; // Переход на страницу успеха
  }

  isCardFormValid(): boolean {
    return this.card.number.length >= 16 && this.card.expiry.length > 0 && this.card.cvv.length === 3;
  }

  isButtonDisabled(): boolean {
    if (this.step === 1) return this.cartItems.length === 0;
    if (this.step === 2)
      return !(this.delivery.name && this.delivery.phone && this.delivery.address);
    if (this.step === 3)
      return !this.paymentMethod ||
        (this.paymentMethod === 'card' && !this.isCardFormValid());
    return false;
  }

  getButtonLabel(): string {
    if (this.step === 1 || this.step === 2) return 'Далее';
    if (this.step === 3) return 'Оплатить';
    return 'Продолжить';
  }

  nextStep(): void {
    if (this.step === 1) {
      this.submitOrder();
    } else if (this.step === 2) {
      this.submitDelivery();
    } else if (this.step === 3) {
      this.submitPayment();
    }
  }

  goBack(prevStep: number) {
    this.step = prevStep;
  }
  removeItem(index: number) {
    this.cartItems.splice(index, 1);
  }

  updateTotal() {
    // Если в будущем будет общая сумма — можно пересчитывать здесь
  }
  increaseQuantity(index: number) {
    this.cartItems[index].quantity++;
  }

  decreaseQuantity(index: number) {
    if (this.cartItems[index].quantity > 1) {
      this.cartItems[index].quantity--;
    }
  }

  getTotalPrice(): number {
    return this.cartItems.reduce((total, item) => {
      return total + item.price * item.quantity;
    }, 0);
  }
  formatExpiryDate() {
    if (!this.card || !this.card.expiry) return;

    // Удаляем все символы, кроме цифр
    let digits = this.card.expiry.replace(/\D/g, '');

    // Ограничиваем максимум 4 цифрами (ММГГ)
    if (digits.length > 4) {
      digits = digits.substring(0, 4);
    }

    // Добавляем слэш после первых двух цифр
    if (digits.length > 2) {
      digits = digits.substring(0, 2) + '/' + digits.substring(2);
    }

    // Обновляем поле
    this.card.expiry = digits;
  }
}
