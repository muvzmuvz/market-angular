import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { TuiRoot } from '@taiga-ui/core';
import { Navbar } from '../../components/navbar/navbar';
import { ProductCard } from '../../components/product-card/product-card';
	import {TuiButton, TuiLink} from '@taiga-ui/core';

// 👉 Обязательно для *ngIf и *ngFor
import { NgIf, NgFor } from '@angular/common';

interface Product {
  id: number;
  name: string;
  price: number;
  description: string;
  images: { path: string }[];
  countProduct: number;
}

@Component({
  selector: 'app-home-page',
  imports: [Navbar,
    ProductCard,
    NgIf,
    NgFor,
    TuiButton,
    TuiLink],
  templateUrl: './home-page.html',
  styleUrl: './home-page.less'
})
export class HomePage {
products: Product[] = [];
  displayedProducts: Product[] = [];
  isLoading = true;
  currentPage = 1;
  itemsPerPage = 24;

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.fetchProducts();
  }

  fetchProducts() {
    this.isLoading = true;

    // 🔧 Пример данных
    this.products = [
      {
        id: 1,
        name: 'Футболка фывфыв фыв фыв',
        price: 1000,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg'}],
      },
      {
        id: 2,
        name: 'Кроссовки',
        price: 3000,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg'}],
      },
            {
        id: 3,
        name: 'Футболка',
        price: 1000,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg'}],
      },
      {
        id: 4,
        name: 'Кроссовки',
        price: 3000,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg'}],
      },
      {
        id: 5,
        name: 'Футболка',
        price: 1000,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg'}],
      },
      {
        id: 6,
        name: 'Кроссовки',
        price: 3000,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg'}],
      },
    ];

    this.updateDisplayedProducts();
    this.isLoading = false;
  }

  updateDisplayedProducts() {
    const start = (this.currentPage - 1) * this.itemsPerPage;
    const end = start + this.itemsPerPage;
    this.displayedProducts = this.products.slice(start, end);
  }

  goToProduct(id: number) {
    this.router.navigate(['/product', id]);
  }

  changePage(page: number) {
    const total = Math.ceil(this.products.length / this.itemsPerPage);
    if (page >= 1 && page <= total) {
      this.currentPage = page;
      this.updateDisplayedProducts();
    }
  }

  get totalPages(): number {
    return Math.ceil(this.products.length / this.itemsPerPage);
  }
}