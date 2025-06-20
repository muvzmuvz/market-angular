import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, shareReplay } from 'rxjs';

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

  constructor(private http: HttpClient) { }

  getConfig(): Observable<SiteConfig> {
    // shareReplay — чтобы не делать повторный HTTP-запрос, если уже был
    return this.http.get<SiteConfig>(this.apiUrl).pipe(shareReplay(1));
  }
}
