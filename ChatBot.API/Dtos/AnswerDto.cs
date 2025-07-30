namespace ChatBot.API.Dtos
{
	public class AnswerDto
	{
		public Guid Id { get; set; }
		public required string Text { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? CanceledAt { get; set; }
		public ReactionType? Reaction { get; set; }
	}
}
