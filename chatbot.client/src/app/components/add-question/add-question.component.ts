import { AfterViewInit, Component, ElementRef, ViewChild } from '@angular/core';
import { QuestionState, QuestionStatus } from 'src/app/models/article.status.model';
import { Question } from 'src/app/models/question.model';
import { QuestionsService } from 'src/app/services/questions.service';

@Component({
  selector: 'app-add-question',
  templateUrl: './add-question.component.html',
  styleUrls: ['./add-question.component.css'],
})
export class AddQuestionComponent implements AfterViewInit {
  public isDisabled: boolean = false;
  public questionText: string = '';
  private question?: Question;

  @ViewChild('questionArea') questionArea!: ElementRef<HTMLTextAreaElement>;

  ngAfterViewInit(): void {
    setTimeout(() => this.initQuestionArea(), 0);
  }

  constructor(private questionService: QuestionsService) {
    this.questionService.questionStatus$.subscribe((questionStatus: QuestionStatus) => {
      if (questionStatus && this.question && questionStatus.questionId === this.question.id) {
        if (questionStatus.state != QuestionState.Created && questionStatus.state != QuestionState.WaitingForAnswer) {
          this.isDisabled = false;
          this.question = undefined;
          this.initQuestionArea();
        }
      }
    });
  }

  public get isButtonDisabled(): boolean {
    return this.isDisabled || !this.questionText || this.questionText.trim().length === 0;
  }

  private initQuestionArea(): void {
    this.questionArea.nativeElement.focus();
    this.questionArea.nativeElement.placeholder = 'Napisz pytanie...';
  }

  public sendQuestion(): void {
    this.isDisabled = true;

    this.questionService.addQuestion(this.questionText.trim()).subscribe({
      next: question => {
        this.questionText = '';
        this.questionArea.nativeElement.placeholder = '';
        this.question = question;
      },
      error: error => {
        this.isDisabled = false;
        this.questionText = '';
      },
    });
  }
}
