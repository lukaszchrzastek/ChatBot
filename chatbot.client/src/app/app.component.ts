import { Component, OnInit } from '@angular/core';
import { SignalRService } from './services/signalr.service';
import { LoadingService } from './services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  apiReady: boolean = false;
  title = 'chatbot';

  constructor(
    private signalRService: SignalRService,
    public loadingService: LoadingService
  ) {}

  ngOnInit() {
    this.signalRService.startConnection();
  }
}
