import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-order-card',
  imports: [CommonModule],
  templateUrl: './order-card.html',
  styleUrl: './order-card.less'
})
export class OrderCard {
  @Input() order: any; // позже можно типизировать
  isExpanded = false;

  toggleExpand() {
    this.isExpanded = !this.isExpanded;
  }

  get stackedItems() {
    if (!this.order?.items) return [];

    const map = new Map();

    for (const item of this.order.items) {
      const key = item.id ?? item.title;

      if (!map.has(key)) {
        map.set(key, { ...item, count: 1 });
      } else {
        const existing = map.get(key);
        existing.count += 1;
        existing.price += item.price;
      }
    }

    const items = Array.from(map.values());
    return this.isExpanded ? items : items.slice(0, 1);
  }
}

