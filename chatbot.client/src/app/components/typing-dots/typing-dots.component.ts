import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-typing-dots',
  templateUrl: './typing-dots.component.html',
  styleUrls: ['./typing-dots.component.css'],
})
export class TypingDotsComponent {
  @Input() visible = true;
}
