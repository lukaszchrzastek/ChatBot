namespace ChatBot.Infrastructure.Services
{
	public interface IChatWorker
	{
		void AddTask(Guid userId, Guid questionId, string questionText);
		void CancelTask(Guid questionId);
		void CancelAllTasks();
	}
}