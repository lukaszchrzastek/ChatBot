import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-validation-error-dialog',
  templateUrl: './validation-error-dialog.component.html',
})
export class ValidationErrorDialogComponent {
  constructor(@Inject(MAT_DIALOG_DATA) public errors: Record<string, string[]>) {}

  get errorKeys(): string[] {
    return Object.keys(this.errors);
  }
}
