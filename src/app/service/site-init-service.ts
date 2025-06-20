import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface SiteInitData {
  siteName: string;
  name: string;
  password: string;
  email: string;
}

@Injectable({
  providedIn: 'root'
})
export class SiteInitService {
  constructor(private http: HttpClient) { }

  initSite(data: SiteInitData): Observable<any> {
    return this.http.post('http://localhost:5042/initials/init', data);
  }

}
