import { Injectable } from '@angular/core';
import * as signalR from '@microsoft/signalr';

import { PartialAnswerMessage } from '../models/partialAnswerMessage.model';
import { QuestionsService } from './questions.service';
import { LoggingService } from './logging.service';
@Injectable({ providedIn: 'root' })
export class SignalRService {
  private hubConnection!: signalR.HubConnection;

  constructor(
    private questionsService: QuestionsService,
    private loggingService: LoggingService
  ) {}

  public startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(`/chathub`, { withCredentials: true })
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => this.loggingService.logInfo('SignalR połaczony'))
      .catch(err => this.loggingService.logError('SignalR bład:', err));

    this.hubConnection.on('PartialAnswerMessage', (partialAnswerMessage: PartialAnswerMessage) =>
      this.questionsService.addPartialAnswerToQuestion(partialAnswerMessage)
    );

    this.hubConnection.onclose(() => this.loggingService.logWarn('SignalR zamknięty'));
  }

  public sendMessage(user: string, message: string): void {
    this.hubConnection
      .invoke('SendMessage', user, message)
      .catch(err => this.loggingService.logError('Send error:', err));
  }
}
