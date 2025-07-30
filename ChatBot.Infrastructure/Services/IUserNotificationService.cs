namespace ChatBot.Infrastructure.Services
{
	public interface IUserNotificationService
	{
		Task SendPartialAnswerMessageAsync(Guid userId, PartialAnswerMessage message, CancellationToken cancellationToken);
	}
}