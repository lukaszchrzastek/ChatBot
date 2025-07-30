import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, timer } from 'rxjs';
import { map, catchError, retryWhen, delayWhen, take, tap, scan } from 'rxjs/operators';
import { LoggingService } from './logging.service';

@Injectable({ providedIn: 'root' })
export class HealthCheckService {
  constructor(
    private http: HttpClient,
    private loggingService: LoggingService
  ) {}

  checkApi(): Observable<boolean> {
    return this.http.get('/health', { responseType: 'text' as const }).pipe(
      map((response: string) => {
        if (response.trim() !== 'Healthy') {
          this.loggingService.logInfo(`Odpowiedź tekstowa ≠ "Healthy": ${response}`);
          throw new Error('Status nie jest "Healthy"');
        }
        this.loggingService.logInfo(`Status OK: ${response}`);
        return true;
      }),
      retryWhen(errors =>
        errors.pipe(
          scan((acc, error) => acc + 1, 0),
          tap(i => this.loggingService.logInfo(`Próba ponowienia: ${i}`)),
          delayWhen(() => timer(1000)),
          take(4)
        )
      ),
      catchError(() => {
        this.loggingService.logError('API nie zwróciło "Healthy" po maksymalnej liczbie prób');
        return of(false);
      })
    );
  }
}
