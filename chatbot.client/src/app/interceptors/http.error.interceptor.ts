import { Injectable } from '@angular/core';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpErrorResponse } from '@angular/common/http';
import { Observable, EMPTY, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ValidationErrorDialogComponent } from '../components/validation-error-dialog/validation-error-dialog.component';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {
  constructor(
    private dialog: MatDialog,
    private snackBar: MatSnackBar
  ) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        const isHealthCheck = req.url.includes('/health');
        const isTimeout504 = error.status === 504;

        if (isHealthCheck && isTimeout504) {
          return EMPTY;
        }
        
        if (error.status === 400 && error.error?.errors) {
          this.dialog.open(ValidationErrorDialogComponent, {
            data: error.error.errors
          });
          return throwError(() => error);
        }
        
        if (!navigator.onLine) {
          this.snackBar.open('Brak połączenia z internetem.', 'Zamknij', {
            duration: 5000,
            horizontalPosition: 'center',
            verticalPosition: 'top',
            panelClass: ['snackbar-error']
          });
        } else {
          this.snackBar.open(`Wystąpił błąd: ${error.message}`, 'Zamknij', {
            duration: 5000,
            horizontalPosition: 'center',
            verticalPosition: 'top',
            panelClass: ['snackbar-warning']
          });
        }
        return throwError(() => error);
      })
    );
  }
}
