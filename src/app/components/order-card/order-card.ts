import { CommonModule } from '@angular/common';
import { Component, Input} from '@angular/core';

@Component({
  selector: 'app-order-card',
  imports: [CommonModule],
  templateUrl: './order-card.html',
  styleUrl: './order-card.less'
})
export class OrderCard {
   @Input() order: any; // позже можно типизировать
    isExpanded = false;

  // Метод переключения состояния
  toggleExpand() {
    this.isExpanded = !this.isExpanded;
  }

  // Получаем список товаров, который нужно показать
  get visibleItems() {
    if (this.isExpanded) {
      return this.order.items; // Показываем все
    } else {
      return this.order.items.slice(0, 1); // Показываем первые 2
    }
  }
}
