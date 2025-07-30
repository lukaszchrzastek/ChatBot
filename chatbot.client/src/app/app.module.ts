import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { APP_INITIALIZER, ErrorHandler, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HealthCheckService } from './services/health-check.service';
import { UserService } from './services/user.service';
import { CookieService } from 'ngx-cookie-service';

import { initializeSequentially } from './app.initializer';
import { AddQuestionComponent } from './components/add-question/add-question.component';
import { QuestionListItemComponent } from './components/question-list-item/question-list-item.component';
import { AnswerListItemComponent } from './components/answer-list-item/answer-list-item.component';
import { ChatMessagesComponent } from './components/chat-messages/chat-messages.component';
import { ReactionButtonsComponent } from './components/reaction-buttons/reaction-buttons.component';
import { TypingDotsComponent } from './components/typing-dots/typing-dots.component';
import { SpinnerComponent } from './components/spinner/spinner.component';
import { LoadingInterceptor } from './interceptors/loading.interceptor';
import { HttpErrorInterceptor } from './interceptors/http.error.interceptor';
import { GlobalErrorHandler } from './handlers/global-error.handler';
import { MatDialogModule } from '@angular/material/dialog';

import { ValidationErrorDialogComponent } from './components/validation-error-dialog/validation-error-dialog.component';

@NgModule({
  declarations: [
    AppComponent,
    AddQuestionComponent,
    QuestionListItemComponent,
    AnswerListItemComponent,
    ChatMessagesComponent,
    ReactionButtonsComponent,
    TypingDotsComponent,
    SpinnerComponent,
    ValidationErrorDialogComponent,
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatListModule,
    MatSnackBarModule,
    MatDialogModule,
  ],
  providers: [
    CookieService,
    {
      provide: APP_INITIALIZER,
      useFactory: initializeSequentially,
      deps: [HealthCheckService, UserService],
      multi: true,
    },    
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoadingInterceptor,
      multi: true,
    },    
    {
      provide: HTTP_INTERCEPTORS,
      useClass: HttpErrorInterceptor,
      multi: true,
    },
    {
      provide: ErrorHandler,
      useClass: GlobalErrorHandler,
    },
  ],
  bootstrap: [AppComponent],
})
export class AppModule {}
