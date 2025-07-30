namespace ChatBot.Infrastructure.Services
{
	public interface IChatTaskRunner
	{
		Task RunAsync(Guid userId, Guid questionId, string questionText, CancellationToken token);
	}
}