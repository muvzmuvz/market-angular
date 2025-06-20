import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, catchError, of } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class InitStatusService {
  constructor(private http: HttpClient) { }

  isInitialized() {
    return this.http.get<{ initializedAt?: string }>('http://localhost:5042/initials/config').pipe(
      map(response => !!response.initializedAt), // если есть дата — сайт установлен
      catchError(() => of(false)) // если ошибка — считаем, что не установлен
    );
  }
}