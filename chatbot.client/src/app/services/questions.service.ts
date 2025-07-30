import { Injectable } from '@angular/core';
import { Question } from '../models/question.model';
import { Answer } from '../models/answer.model';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { ChatBotService } from './chat-bot.service';
import { PartialAnswerMessage } from '../models/partialAnswerMessage.model';
import { QuestionState, QuestionStatus } from '../models/article.status.model';
import { Guid } from 'guid-typescript';
import { ReactionType } from '../models/reaction.type';
import { Data } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class QuestionsService {
  private questions: Question[] = [];
  private questionsSubject = new BehaviorSubject<Question[]>([]);
  private questionStatusSubject = new BehaviorSubject<QuestionStatus>({
    questionId: Guid.create(),
    state: QuestionState.Created,
  });
  public questionStatus$: Observable<QuestionStatus> = this.questionStatusSubject.asObservable();

  constructor(private chatBotService: ChatBotService) {}

  private updateQuestionStatus(questionId: Guid, state: QuestionState): void {
    this.questionStatusSubject.next({ questionId, state });
  }

  public getQuestions(): Observable<Question[]> {
    return this.questionsSubject.asObservable();
  }

  public addQuestion(questionText: string): Observable<Question> {
    return this.chatBotService.askQuestion(questionText).pipe(
      tap((question: Question) => {
        this.questions.push(question);
        this.emitQuestions();
        this.updateQuestionStatus(question.id, QuestionState.Created);
      })
    );
  }

  addPartialAnswerToQuestion(partialAnswerMessage: PartialAnswerMessage): void {
    const question = this.questions.find(q => q.id === partialAnswerMessage.questionId);
    if (question) {
      if (!question.answer) {
        question.answer = {
          id: partialAnswerMessage.id,
          text: partialAnswerMessage.text,
          createdAt: new Date(),
          isCompleted: partialAnswerMessage.isFinalChunk,
        };
      } else {
        question.answer.text += `\n\n${partialAnswerMessage.text}`;
        question.answer.isCompleted = partialAnswerMessage.isFinalChunk;
      }
      this.updateQuestionStatus(
        question.id,
        partialAnswerMessage.isFinalChunk ? QuestionState.Completed : QuestionState.WaitingForAnswer
      );
      this.emitQuestions();
    }
  }

  public loadQuestions(): Observable<void> {
    return this.chatBotService.getQuestions().pipe(
      tap(questions => {
        this.questions = questions;
        this.emitQuestions();
      }),
      map(() => void 0)
    );
  }

  public cancel(questionId: Guid): Observable<void> {
    return this.chatBotService.cancel(questionId).pipe(
      tap(() => {
        const question = this.questions.find(q => q.id === questionId);
        if (question?.answer) {
          question.answer.isCompleted = true;
          question.answer.canceledAt = new Date();
          this.emitQuestions();
        }
        this.updateQuestionStatus(questionId, QuestionState.Interrupted);
      }),
      map(() => void 0)
    );
  }

  public likeAnswer(questionId: Guid) {
    this.chatBotService.rateAnswer(questionId, ReactionType.Like).subscribe({
      next: () => {
        console.log('Pytanie polubione:', questionId);
      },
      error: error => {
        console.error('Błąd podczas polubienia pytania:', error);
      },
    });
  }

  public dislikeAnswer(questionId: Guid) {
    this.chatBotService.rateAnswer(questionId, ReactionType.Dislike).subscribe({
      next: () => {
        console.log('Pytanie odrzucone:', questionId);
      },
      error: error => {
        console.error('Błąd podczas odrzucania pytania:', error);
      },
    });
  }

  private emitQuestions(): void {
    this.questionsSubject.next([...this.questions]);
  }
}
