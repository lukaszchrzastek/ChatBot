import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Question } from '../models/question.model';
import { map, Observable } from 'rxjs';
import { ReactionType } from '../models/reaction.type';

@Injectable({
  providedIn: 'root',
})
export class ChatBotService {
  private apiUrl = '/api/chatbot';

  constructor(private http: HttpClient) {}

  public askQuestion(questionText: string): Observable<Question> {
    const dto = { QuestionText: questionText };
    return this.http.post<Question>(`${this.apiUrl}/add-question`, dto);
  }

  public getQuestions(): Observable<Question[]> {
    return this.http.get<Question[]>(`${this.apiUrl}/questions`).pipe(
      map(result => {
        result.forEach(q => {
          if (q.answer) {
            q.answer.isCompleted = true;
          }
        });
        return result;
      })
    );
  }

  public cancel(questionId: Guid): Observable<void> {
    const dto = { QuestionId: questionId };
    return this.http.post<void>(`${this.apiUrl}/cancel-question`, dto);
  }

  public rateAnswer(questionId: Guid, reactionType: ReactionType): Observable<void> {
    const dto = { QuestionId: questionId, Reaction: reactionType };
    return this.http.post<void>(`${this.apiUrl}/rate-answer`, dto);
  }
}
