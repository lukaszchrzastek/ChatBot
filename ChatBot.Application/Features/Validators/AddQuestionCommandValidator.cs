using ChatBot.Application.Features.Questions.Commands;
using ChatBot.Domain.Repositories;
using FluentValidation;
namespace ChatBot.Application.Features.Validators
{
	public class AddQuestionCommandValidator : AbstractValidator<AddQuestionCommand>
	{
		public AddQuestionCommandValidator(IUserRepository userRepository)
		{
			RuleFor(x => x.UserId)
			.NotEmpty().WithMessage("Identyfikator użytkownika nie może być pusty.")
			.MustAsync(async (userId, cancellation) =>
			{
				return await userRepository.GetUserAsync(userId) is not null;
			})
			.WithMessage("Użytkownik o podanym ID nie istnieje.");

			RuleFor(x => x.QuestionText)
				.NotEmpty().WithMessage("Treść pytania nie może być pusta")
				.MaximumLength(100).WithMessage("Treść pytania jest za długa");
		}
	}
}
