import { Guid } from 'guid-typescript';
import { ReactionType } from './reaction.type';
export interface Answer {
  id: Guid;
  text: string;
  createdAt: Date;
  isCompleted: boolean;
  reaction?: ReactionType;
  canceledAt?: Date;
}
