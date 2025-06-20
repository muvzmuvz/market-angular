import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, catchError, of, shareReplay } from 'rxjs';

interface InitConfig {
  initializedAt?: string;
}

@Injectable({
  providedIn: 'root'
})
export class InitStatusService {
  private apiUrl = 'http://localhost:5042/initials/config';

  constructor(private http: HttpClient) {}

  isInitialized(): Observable<boolean> {
    return this.http.get<{ initializedAt?: string }>(this.apiUrl).pipe(
      map(response => !!response.initializedAt),
      catchError(() => of(false)) // При ошибке считаем, что не инициализировано
    );
  }
}
