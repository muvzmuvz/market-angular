import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class Fetprofile {
  constructor(private http: HttpClient) {}

  getUserRole(): Observable<string> {
    return this.http.get<any>('http://localhost:5042/accounts/').pipe(
      map(user => {
        const role = user?.roles?.[0] || 'Guest';
        return role;
      })
    );
  }
}
