import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of, throwError } from 'rxjs';
import { tap, map, catchError } from 'rxjs/operators';
import { CookieService } from 'ngx-cookie-service';

import { Guid } from 'guid-typescript';
import { User } from '../models/user.model';
@Injectable({
  providedIn: 'root',
})
export class UserService {
  private apiUrl = '/api/user';
  constructor(
    private http: HttpClient,
    private cookieService: CookieService
  ) {}

  getUserId(): string {
    return this.cookieService.get('user-id');
  }

  createUser(): Observable<void> {
    return this.http.post<{ id: Guid }>(`${this.apiUrl}/create-user`, {}).pipe(
      tap(response => {
        const expiresDate = new Date();
        expiresDate.setFullYear(expiresDate.getFullYear() + 1);
        this.cookieService.set('user-id', response.id.toString(), {
          expires: expiresDate,
          path: '/',
          sameSite: 'Strict',
          secure: true,
        });
      }),
      map(() => undefined)
    );
  }

  exist(userId: string): Observable<boolean> {
    const params = new HttpParams().set('userId', userId);
    return this.http.get<User>(`${this.apiUrl}/get-user`, { params }).pipe(
      map(() => true),
      catchError(err => {
        if (err.status === 404) {
          return of(false);
        }
        return throwError(() => err);
      })
    );
  }
}
