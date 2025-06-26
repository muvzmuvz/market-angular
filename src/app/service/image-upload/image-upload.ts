import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ImageUploadService {
  private apiUrl = 'http://localhost:5042/accounts/image/me';  // URL endpoint

  constructor(private http: HttpClient) { }

  uploadImage(imageBase64: string): Observable<any> {
    const body = {
      ImageBase64: imageBase64
    };

    const headers = new HttpHeaders({
      'Content-Type': 'application/json'
    });

    return this.http.put(this.apiUrl, body, { headers });
  }
}
