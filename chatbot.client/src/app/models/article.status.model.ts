import { Guid } from 'guid-typescript';

export enum QuestionState {
  Created = 'created',
  WaitingForAnswer = 'waiting-for-answer',
  Completed = 'completed',
  Interrupted = 'interrupted',
  Error = 'error',
}

export interface QuestionStatus {
  questionId: Guid;
  state: QuestionState;
}
