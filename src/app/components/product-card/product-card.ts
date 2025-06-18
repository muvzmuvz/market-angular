import { Component, Input } from '@angular/core';
import { TuiButton, TuiLink } from '@taiga-ui/core';
import { TuiBadge } from '@taiga-ui/kit';

@Component({
  selector: 'app-product-card',
  imports: [TuiButton, TuiLink, TuiBadge],
  templateUrl: './product-card.html',
  styleUrl: './product-card.less'
})
export class ProductCard {
  @Input() product: any;
  order: any;
  addToCart() {
    console.log('Товар добавлен:', this.product);
    // Здесь можно вызвать Output-событие или сервис
  }

  getDiscountedPrice(price: number, discount: number): number {
    if (!discount || discount <= 0) return price;
    return Math.round(price * (1 - discount / 100));
  }

  hasDiscount(product: any): boolean {
    return !!product.discount && Number(product.discount) > 0;
  }

}
