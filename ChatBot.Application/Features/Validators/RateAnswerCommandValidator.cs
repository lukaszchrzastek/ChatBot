using ChatBot.Application.Features.Questions.Commands;
using ChatBot.Domain.Repositories;
using FluentValidation;

namespace ChatBot.Application.Features.Validators
{
	public class RateAnswerCommandValidator : AbstractValidator<RateAnswerCommand>
	{
		public RateAnswerCommandValidator(
			IUserRepository userRepository,
			IChatRepository chatRepository
			)
		{
			RuleFor(x => x.QuestionId)
				.NotEmpty().WithMessage("Identyfikator pytania nie może być pusty.")
				.MustAsync(async (questionId, cancellation) =>
				{
					return await chatRepository.GetQuestionByIdAsync(questionId) is not null;
				})
				.WithMessage("Pytanie o podanym ID nie istnieje.");

			RuleFor(x => x.UserId)
				.NotEmpty().WithMessage("Identyfikator użytkownika nie może być pusty.")
				.MustAsync(async (userId, cancellation) =>
				{
					return await userRepository.GetUserAsync(userId) is not null;
				})
				.WithMessage("Użytkownik o podanym ID nie istnieje.");

			RuleFor(x => x.Reaction)
				.IsInEnum().WithMessage("Nieprawidłowy typ reakcji.");
		}
	}

}
