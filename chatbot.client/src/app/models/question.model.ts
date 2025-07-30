import { Guid } from 'guid-typescript';
import { Answer } from './answer.model';
export interface Question {
  id: Guid;
  text: string;
  createdAt: Date;
  answer?: Answer;
}
