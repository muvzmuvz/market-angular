import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Shop {
  name: string;
  description: string;
  userId: string;
}

export interface User {
  id: string;
  email: string;
  // добавь другие поля при необходимости
}
@Injectable({
  providedIn: 'root'
})
export class ShopService {
  private apiUrl = 'http://localhost:5042';

  constructor(private http: HttpClient) { }

  // Получить всех пользователей
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/accounts`);
  }

  // Создать магазин
  createShop(shop: Shop): Observable<any> {
    return this.http.post(`${this.apiUrl}/shops`, shop);
  }
}


