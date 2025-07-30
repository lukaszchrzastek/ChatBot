import { Component, ElementRef, ViewChild } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs';
import { Question } from 'src/app/models/question.model';
import { QuestionsService } from 'src/app/services/questions.service';

@Component({
  selector: 'app-chat-messages',
  templateUrl: './chat-messages.component.html',
  styleUrls: ['./chat-messages.component.css'],
})
export class ChatMessagesComponent {
  public questions$: Observable<Question[]> = this.questionService.getQuestions();
  public questionsList: Question[] = [];

  @ViewChild('chatMessages') chatMessages!: ElementRef;

  ngAfterViewChecked() {
    if (this.chatMessages) {
      this.chatMessages.nativeElement.scrollTop = this.chatMessages.nativeElement.scrollHeight;
    }
  }

  constructor(private questionService: QuestionsService) {
    this.questions$.subscribe(questions => (this.questionsList = questions));

    this.questionService.loadQuestions().subscribe({
      next: questions => {},
      error: error => {
        console.error('Błąd podczas ładowania pytań:', error);
      },
    });
  }

  public onLiked(questionId: Guid): void {
    console.log('Polubiono odpowiedź dla pytania:', questionId);
    this.questionService.likeAnswer(questionId);
  }

  public onDisliked(questionId: Guid): void {
    console.log('Odpowiedź nie spodobała się dla pytania:', questionId);
    this.questionService.dislikeAnswer(questionId);
  }

  public onCanceled(questionId: Guid): void {
    console.log('Anulowano odpowiedź dla pytania:', questionId);
    this.questionService.cancel(questionId).subscribe({
      next: () => {},
    });
  }
}
