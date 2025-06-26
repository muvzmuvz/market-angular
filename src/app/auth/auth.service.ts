import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { OidcSecurityService } from 'angular-auth-oidc-client';
import { map, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private role: string = '';

  constructor(private http: HttpClient) { }

  // ⛳️ ВАЖНО: метод должен возвращать Observable<string>
  fetchUserRole(): Observable<string> {
    return this.http.get<any[]>('http://localhost:5042/accounts').pipe(
      map((accounts: string | any[]) => {
        if (accounts.length > 0 && accounts[0].roles?.length > 0) {
          this.role = accounts[0].roles[0];
        } else {
          this.role = 'Guest';
        }
        return this.role;
      })
    );
  }

  getRole(): string {
    return this.role;
  }

  isAdmin(): boolean {
    return this.role === 'Administrator';
  }

  isUser(): boolean {
    return this.role === 'User';
  }
}