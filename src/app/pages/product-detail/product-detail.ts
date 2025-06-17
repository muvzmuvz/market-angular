import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule, Location } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TuiLoader } from '@taiga-ui/core';
import { Navbar } from 'src/app/components/navbar/navbar'; // standalone Navbar
import { Router } from '@angular/router';
import { TuiAvatar} from '@taiga-ui/kit';

interface ProductImage {
  path: string;
}

interface Product {
  id: string | null;
  name: string;
  description: string;
  price: number;
  characteristic: string;
  user: { name: string };
  images: ProductImage[];
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
  imports: [CommonModule, FormsModule, Navbar, TuiLoader, TuiAvatar] // ✅ правильный список
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

constructor(
  private route: ActivatedRoute,
  private location: Location,
  private router: Router
) {}

goBack(): void {
  console.log('goBack called');

  if (window.history.length > 1) {
    console.log('using location.back()');
    this.location.back();
  } else {
    console.log('fallback: navigating to /');
    this.router.navigate(['/']);
  }
}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    this.loadProductById(id);
  }

  get modalImage(): string {
    return this.product?.images?.[this.activeIndex]?.path || '';
  }

  loadProductById(id: string | null): void {
    this.isLoading = true;

    // ⚠️ Заменить на реальный запрос при подключении API
    setTimeout(() => {
      this.product = {
        id,
        name: `Футболка Angular (ID ${id})`,
        description: 'Удобная и стильная футболка с логотипом Angular.',
        price: 1999,
        characteristic: 'Размеры: S, M, L, XL\nЦвет: Красный',
        user: { name: 'Анна' },
        images: [
          {
            path: 'https://ae04.alicdn.com/kf/Sf73c6b0b761343abb82901fa276c7c36x.jpg_480x480.jpg'
          },
          {
            path: 'https://modano.ru/wa-data/public/shop/products/08/15/1508/images/203949/203949.635x953@2x.jpg'
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
          userAvatar: 'https://i.ytimg.com/vi/Bs3-aQdqrW4/hq720.jpg?sqp=-oaymwEhCK4FEIIDSFryq4qpAxMIARUAAAAAGAElAADIQj0AgKJD&rs=AOn4CLBll8FGXj_7pmm71QvR6bweo1BNmw'
        }
      ];

      this.isLoading = false;
    }, 1000);
  }

  addToCart(): void {
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
}
