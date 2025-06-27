import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { isPlatformBrowser } from '@angular/common';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

export interface User {
  id: string;
  roles: string[];
  // можно добавить другие свойства, которые приходят с сервера
}

export interface Shop {
  name: string;
  description: string;
  userId: string;
  // другие поля магазина
}

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  private apiUrl = 'http://localhost:5042';

  constructor(
    private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {}

  private getAccessTokenFromCookie(): string | null {
    if (!isPlatformBrowser(this.platformId)) return null;

    const match = document.cookie.match(/(^| )access_token=([^;]+)/);
    return match ? match[2] : null;
  }

  // Получаем данные текущего пользователя
  getMe(): Observable<User | null> {
    const token = this.getAccessTokenFromCookie();

    if (!token) {
      console.warn('Токен доступа не найден в cookie');
      return of(null);
    }

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });

    return this.http.get<User>(`${this.apiUrl}/accounts/me`, { headers }).pipe(
      catchError(err => {
        if (err.error && typeof err.error === 'string' && err.error.startsWith('<!DOCTYPE')) {
          console.warn('Получена HTML страница вместо JSON — возможно, пользователь не авторизован');
        } else {
          console.error('Ошибка при получении данных пользователя:', err);
        }
        return of(null);
      })
    );
  }

  // Метод для создания магазина
  createShop(shop: Shop): Observable<any> {
    const token = this.getAccessTokenFromCookie();

    if (!token) {
      console.warn('Токен доступа не найден, невозможно создать магазин');
      return of(null);
    }

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      Authorization: `Bearer ${token}`,
    });

    return this.http.post(`${this.apiUrl}/shops`, shop, { headers }).pipe(
      catchError(err => {
        console.error('Ошибка при создании магазина:', err);
        return of(null);
      })
    );
  }
}
