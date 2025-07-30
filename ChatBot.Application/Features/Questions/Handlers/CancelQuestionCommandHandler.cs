using ChatBot.Application.Features.Questions.Commands;
using ChatBot.Domain.Repositories;
using ChatBot.Infrastructure.Services;
using MediatR;

namespace ChatBot.Application.Features.Questions.Handlers
{
	public class CancelQuestionCommandHandler(
		IChatRepository chatRepository,
		IChatWorker chatWorker
		) : IRequestHandler<CancelQuestionCommand, Unit>
	{
		public async Task<Unit> Handle(CancelQuestionCommand request, CancellationToken cancellationToken)
		{
			_ = await chatRepository.GetQuestionByIdAsync(request.QuestionId);

			chatWorker.CancelTask(request.QuestionId);

			return Unit.Value;
		}
	}
}
