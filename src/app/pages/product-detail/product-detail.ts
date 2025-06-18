import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule, Location } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TuiLoader } from '@taiga-ui/core';
import { Navbar } from 'src/app/components/navbar/navbar';
import { Router } from '@angular/router';
import { TuiAvatar } from '@taiga-ui/kit';
import { TuiBadge } from '@taiga-ui/kit';
import { TuiButton } from '@taiga-ui/core';

interface ProductImage {
  path: string;
}

interface Product {
  id: string | null;
  name: string;
  description: string;
  discountPercent?: number;
  price: number;
  characteristic: string;
  user: { name: string };
  images: ProductImage[];
  colors?: string[];
  sizes?: string[];
  availability?: string;
  isFav?: boolean;
  similar?: { name: string; image: string; price: number }[];
}

interface Comment {
  id: number;
  user: { name: string };
  description: string;
  productEvaluation: number;
  dateCreated: string;
  userAvatar?: string;
}

@Component({
  selector: 'app-product-detail',
  standalone: true,
  templateUrl: './product-detail.html',
  styleUrls: ['./product-detail.less'],
  imports: [CommonModule, FormsModule, Navbar, TuiLoader, TuiAvatar, TuiBadge, TuiButton]
})
export class ProductDetailComponent implements OnInit {
  product: Product | null = null;
  isLoading = true;
  comments: Comment[] = [];
  newComment = '';
  productEvaluation = 5;
  isModalOpen = false;
  activeIndex = 0;
  isAdded = false;

  selectedColor = '';
  selectedSize = '';

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private router: Router
  ) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.loadProductById(id);
  }

  goBack(): void {
    if (window.history.length > 1) {
      this.location.back();
    } else {
      this.router.navigate(['/']);
    }
  }

  loadProductById(id: string | null): void {
    this.isLoading = true;

    setTimeout(() => {
      this.product = {
        id,
        name: `Футболка Angular (ID ${id})`,
        description: 'Удобная и стильная футболка с логотипом Angular.',
        price: 1999,
        discountPercent: 30,
        characteristic: 'Размеры: S, M, L, XL\nЦвет: Красный',
        user: { name: 'Анна' },
        images: [
          { path: 'https://ae04.alicdn.com/kf/Sf73c6b0b761343abb82901fa276c7c36x.jpg_480x480.jpg' },
          { path: 'https://c1.cprnt.com/storage/p/t1/aw/lwh/8d/bcc/c35e91145a61f1f09fe24b3b31d/bb93552490a04d75aea502d2c1ed3257.webp' },
          { path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' },
          { path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' },
          { path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg' },
        ],
        colors: ['Красный', 'Чёрный', 'Белый'],
        sizes: ['S', 'M', 'L', 'XL'],
        availability: 'В наличии',
        isFav: false,
        similar: [
          {
            name: 'Футболка React',
            image: 'https://c1.cprnt.com/storage/p/t1/aw/lwh/8d/bcc/c35e91145a61f1f09fe24b3b31d/bb93552490a04d75aea502d2c1ed3257.webp',
            price: 1899
          },
          {
            name: 'Футболка Vue',
            image: 'https://c1.cprnt.com/storage/p/t1/aw/lwh/8d/bcc/c35e91145a61f1f09fe24b3b31d/bb93552490a04d75aea502d2c1ed3257.webp',
            price: 1799
          }
        ]
      };

      this.comments = [
        {
          id: 1,
          user: { name: 'Иван' },
          description: 'Очень понравилось!',
          productEvaluation: 5,
          dateCreated: '2024-05-10',
          userAvatar: 'https://i.ytimg.com/vi/Bs3-aQdqrW4/hq720.jpg'
        }
      ];

      this.selectedColor = this.product.colors?.[0] || '';
      this.selectedSize = this.product.sizes?.[0] || '';
      this.isLoading = false;
    }, 1000);
  }

  get modalImage(): string {
    return this.product?.images?.[this.activeIndex]?.path || '';
  }

  addToCart(): void {
    if (!this.selectedColor || !this.selectedSize) {
      alert('Пожалуйста, выберите цвет и размер.');
      return;
    }

    this.isAdded = true;
    setTimeout(() => (this.isAdded = false), 2000);
  }

  postComment(): void {
    if (!this.newComment.trim()) return;

    this.comments.unshift({
      id: Date.now(),
      user: { name: 'Вы' },
      description: this.newComment,
      productEvaluation: this.productEvaluation,
      dateCreated: new Date().toISOString()
    });

    this.newComment = '';
    this.productEvaluation = 5;
  }

  getStars(count: number): string {
    return '★'.repeat(count) + '☆'.repeat(5 - count);
  }

  formatDate(date: string): string {
    return new Date(date).toLocaleDateString('ru-RU', {
      year: 'numeric',
      month: 'long',
      day: '2-digit'
    });
  }

  get hasDiscount(): boolean {
    return !!this.product?.discountPercent && this.product.discountPercent > 0;
  }

  get discountedPrice(): number {
    if (!this.product || !this.hasDiscount) return this.product?.price || 0;
    const discount = (this.product.price * this.product.discountPercent!) / 100;
    return Math.round(this.product.price - discount);
  }

  toggleFavorite(): void {
    if (this.product) {
      this.product.isFav = !this.product.isFav;
    }
  }

  share(): void {
    if (navigator.share) {
      navigator.share({
        title: this.product?.name,
        text: 'Смотрите этот товар!',
        url: window.location.href
      }).catch(console.error);
    } else {
      alert('Ваш браузер не поддерживает функцию «Поделиться»');
    }
  }

  viewSimilar(): void {
    alert('Переход к похожему товару...');
  }

  
}
