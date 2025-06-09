import { Component, Input } from '@angular/core';
import {TuiButton, TuiLink} from '@taiga-ui/core';

@Component({
  selector: 'app-product-card',
  imports: [TuiButton, TuiLink],
  templateUrl: './product-card.html',
  styleUrl: './product-card.less'
})
export class ProductCard {
 @Input() product: any;
 addToCart() {
  console.log('Товар добавлен:', this.product);
  // Здесь можно вызвать Output-событие или сервис
}
}
