import { Guid } from 'guid-typescript';
export interface PartialAnswerMessage {
  questionId: Guid;
  id: Guid;
  sequence: number;
  text: string;
  isFinalChunk: boolean;
}
