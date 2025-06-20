import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap, shareReplay } from 'rxjs';
import { Title } from '@angular/platform-browser';

export interface SiteConfig {
  siteName: string;
  id: string;
  initializedAt: string;
}

@Injectable({
  providedIn: 'root'
})
export class SiteConfigService {
  private apiUrl = 'http://localhost:5042/initials/config';

  // Кэшируем результат, чтобы не делать повторный HTTP-запрос
  private cachedConfig$!: Observable<SiteConfig>;

  constructor(
    private http: HttpClient,
    private titleService: Title // ← добавили Title-сервис
  ) {}

  getConfig(): Observable<SiteConfig> {
    if (!this.cachedConfig$) {
      this.cachedConfig$ = this.http.get<SiteConfig>(this.apiUrl).pipe(
        tap(config => {
          // Устанавливаем <title> после получения siteName
          this.titleService.setTitle(config.siteName);
        }),
        shareReplay(1) // кэшируем значение
      );
    }

    return this.cachedConfig$;
  }
}
