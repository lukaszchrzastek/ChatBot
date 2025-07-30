import { Component, Input, Output, EventEmitter } from '@angular/core';
import { ReactionType } from 'src/app/models/reaction.type';

@Component({
  selector: 'app-reaction-buttons',
  templateUrl: './reaction-buttons.component.html',
  styleUrls: ['./reaction-buttons.component.css'],
})
export class ReactionButtonsComponent {
  @Input() reaction?: ReactionType;
  @Output() like = new EventEmitter<void>();
  @Output() dislike = new EventEmitter<void>();

  ReactionType = ReactionType;

  isPulsingLike = false;
  isPulsingDislike = false;

  onLike() {
    if (this.reaction === ReactionType.Like) return;

    this.isPulsingLike = true;
    this.reaction = ReactionType.Like;
    setTimeout(() => (this.isPulsingLike = false), 1000);
    this.like.emit();
  }

  onDislike() {
    if (this.reaction === ReactionType.Dislike) return;

    this.isPulsingDislike = true;
    this.reaction = ReactionType.Dislike;
    setTimeout(() => (this.isPulsingDislike = false), 1000);
    this.dislike.emit();
  }
}
