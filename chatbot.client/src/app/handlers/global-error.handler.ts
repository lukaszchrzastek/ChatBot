import { ErrorHandler, inject, Injectable } from '@angular/core';
import { LoggingService } from '../services/logging.service';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {
  private loggingService = inject(LoggingService);

  handleError(error: any): void {
    this.loggingService.logError('Wystąpił nieoczekiwany błąd:', error);
    alert('Wystąpił nieoczekiwany błąd.');
  }
}
