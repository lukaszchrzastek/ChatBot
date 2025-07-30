import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Answer } from 'src/app/models/answer.model';

@Component({
  selector: 'app-answer-list-item',
  templateUrl: './answer-list-item.component.html',
  styleUrls: ['./answer-list-item.component.css'],
})
export class AnswerListItemComponent {
  @Input() questionId!: Guid;
  @Input() answer!: Answer;
  @Output() liked = new EventEmitter<Guid>();
  @Output() disliked = new EventEmitter<Guid>();
  @Output() canceled = new EventEmitter<Guid>();

  onLikeAnswer() {
    this.liked.emit(this.questionId);
  }

  onDislikeAnswer() {
    this.disliked.emit(this.questionId);
  }

  onCancelAnswer() {
    this.canceled.emit(this.questionId);
  }
}
