import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class FetchProfile {
  private roleSubject = new BehaviorSubject<string>('Guest');

  constructor(
    private http: HttpClient,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    // Проверяем, запущен ли код в браузере
    if (isPlatformBrowser(this.platformId)) {
      this.loadUserRole();
    }
  }

  private loadUserRole() {
    const token = this.getAccessTokenFromCookie();

    if (!token) {
      console.warn('Токен доступа не найден в cookie');
      this.roleSubject.next('Guest');
      return;
    }

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });

    this.http.get<any>('http://localhost:5042/accounts/me', { headers }).pipe(
      map(user => user?.roles?.[0] || 'Guest'),
      catchError(err => {
        if (err.error && typeof err.error === 'string' && err.error.startsWith('<!DOCTYPE')) {
          console.warn('Получена HTML страница вместо JSON — вероятно, пользователь не авторизован');
        } else {
          console.error('Ошибка при загрузке роли пользователя:', err);
        }
        return of('Guest');
      })
    ).subscribe(role => this.roleSubject.next(role));
  }

  private getAccessTokenFromCookie(): string | null {
    if (!isPlatformBrowser(this.platformId)) return null;

    const match = document.cookie.match(/(^| )access_token=([^;]+)/);
    return match ? match[2] : null;
  }

  get role$(): Observable<string> {
    return this.roleSubject.asObservable();
  }

  refreshUserRole() {
    if (isPlatformBrowser(this.platformId)) {
      this.loadUserRole();
    }
  }
}
