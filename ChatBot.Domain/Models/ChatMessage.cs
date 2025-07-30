namespace ChatBot.Domain.Models
{
	public class ChatMessage
	{
		public Guid UserId { get; init; }
		public Guid QuestionId { get; init; }
		public required string QuestionText { get; init; }
	}
}
