import { Component } from '@angular/core';
import { Router, RouterOutlet } from '@angular/router';
import { TuiRoot } from '@taiga-ui/core';
import { Navbar } from '../../components/navbar/navbar';
import { ProductCard } from '../../components/product-card/product-card';
import { TuiButton, TuiLink } from '@taiga-ui/core';
import { TuiPagination } from '@taiga-ui/kit';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

// 👉 Обязательно для *ngIf и *ngFor
import { NgIf, NgFor } from '@angular/common';

interface Product {
  id: number;
  name: string;
  discount?: number;
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
    TuiLink, TuiPagination],
  templateUrl: './home-page.html',
  styleUrl: './home-page.less',
  standalone: true,  // если используешь standalone компоненты
})
export class HomePage {
  products: Product[] = [];
  displayedProducts: Product[] = [];
  isLoading = true;
  currentPage = 1;
  itemsPerPage = 24;

  constructor(private router: Router) { }
  isAuthenticated = false;
  accessToken = '';
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
        discount: 30,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 2,
        name: 'Кроссовки',
        price: 3000,
        discount: 30,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 3,
        name: 'Футболка',
        price: 1000,
        discount: 30,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 4,
        name: 'Кроссовки',
        price: 3000,
        discount: 30,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 5,
        name: 'Футболка',
        price: 1000,
        discount: 30,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 6,
        name: 'Кроссовки',
        price: 3000,
        discount: 30,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 7,
        name: 'Футболка фывфыв фыв фыв',
        price: 1000,
        discount: 30,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 8,
        name: 'Кроссовки',
        price: 3000,
        discount: 30,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 9,
        name: 'Футболка',
        price: 1000,
        discount: 30,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 10,
        name: 'Кроссовки',
        price: 3000,
        discount: 30,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 11,
        name: 'Футболка',
        price: 1000,
        discount: 30,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 12,
        name: 'Кроссовки',
        price: 3000,
        discount: 30,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 13,
        name: 'Футболка фывфыв фыв фыв',
        price: 1000,
        discount: 30,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 14,
        name: 'Кроссовки',
        price: 3000,
        discount: 30,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 15,
        name: 'Футболка',
        price: 1000,
        discount: 30,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 16,
        name: 'Кроссовки',
        price: 3000,
        discount: 30,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 17,
        name: 'Футболка',
        price: 1000,
        discount: 30,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 18,
        name: 'Кроссовки',
        price: 3000,
        discount: 30,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 19,
        name: 'Футболка фывфыв фыв фыв',
        price: 1000,
        discount: 30,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 20,
        name: 'Кроссовки',
        price: 3000,
        discount: 30,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 21,
        name: 'Футболка',
        price: 1000,
        discount: 30,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 22,
        name: 'Кроссовки',
        price: 3000,
        discount: 30,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 23,
        name: 'Футболка',
        price: 1000,
        discount: 30,
        description: 'Белая футболка',
        countProduct: 10,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 24,
        name: 'Кроссовки',
        price: 3000,
        discount: 30,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
      },
      {
        id: 25,
        name: 'Кроссовки',
        price: 3000,
        discount: 30,
        description: 'Удобные кроссовки',
        countProduct: 5,
        images: [{ path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' }],
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



  get totalPages(): number {
    return Math.ceil(this.products.length / this.itemsPerPage);
  }

  changePage(index: number) {
    this.currentPage = index + 1;
    this.updateDisplayedProducts();
  }
}