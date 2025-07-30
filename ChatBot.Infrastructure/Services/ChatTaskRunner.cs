using ChatBot.Domain.Models;
using ChatBot.Domain.Repositories;
using ChatBot.Domain.Services;
using ChatBot.Infrastructure.Extensions;
using Microsoft.Extensions.Logging;
using System.Text;

namespace ChatBot.Infrastructure.Services
{
	public class ChatTaskRunner(
		IAiChatService aiChatService,
		IUserNotificationService userNotificationService,
		IChatRepository chatRepository,
		TimeProvider timeProvider,
		ILogger<ChatTaskRunner> logger) : IChatTaskRunner
	{
		public async Task RunAsync(
			Guid userId,
			Guid questionId,
			string questionText,
			CancellationToken token)
		{
			StringBuilder stringBuilder = new();
			var answerId = new Guid();
			var createdAt = timeProvider.GetUtcNow().DateTime;

			try
			{
				logger.LogInformation("Rozpoczynam zadanie {QuestionId} dla użytkownika {UserId}.", questionId, userId);

				var reader = aiChatService.Ask(questionText, token);

				int sequence = 0;


				await foreach (var (message, isLast) in reader.ReadAllAsync(token).WithLastFlag(token))
				{
					try
					{
						if (stringBuilder.Length > 0)
							stringBuilder.AppendLine(string.Empty);

						stringBuilder.Append(message);

						var partialAnswerMessage = new PartialAnswerMessage(questionId, answerId, sequence, message, isLast);

						await userNotificationService.SendPartialAnswerMessageAsync(userId, partialAnswerMessage, token);
						sequence++;
					}
					catch (Exception ex)
					{
						logger.LogWarning(ex, "Błąd wysyłania wiadomości dla użytkownika {UserId}", userId);
					}
				}

				var answer = Answer.Create(answerId, stringBuilder.ToString(), createdAt);

				var question = await chatRepository.GetQuestionByIdAsync(questionId) ?? throw new Exception("Pytanie nie istnieje");
				question.AddAnswer(answer);
				await chatRepository.UpdateQuestionAsync(question);

				logger.LogInformation("Zadanie {QuestionId} zakończone.", questionId);
			}
			catch (OperationCanceledException)
			{

				try
				{
					var answer = Answer.Create(answerId, stringBuilder.ToString(), createdAt, null, timeProvider.GetUtcNow().DateTime);
					var question = await chatRepository.GetQuestionByIdAsync(questionId) ?? throw new Exception("Pytanie nie istnieje");
					question.AddAnswer(answer);
					await chatRepository.UpdateQuestionAsync(question);
					logger.LogInformation("Zadanie {QuestionId} zostało anulowane.", questionId);
				}
				catch (Exception ex)
				{
					logger.LogError(ex, "Błąd podczas anulowania zadania {QuestionId}", questionId);
				}
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "Błąd w zadaniu {QuestionId}");
			}
		}
	}
}
