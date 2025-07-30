namespace ChatBot.API.Dtos
{
	public class RateAnswerDto
	{
		public Guid QuestionId { get; set; }

		public ReactionType Reaction { get; set; }
		public DateTime? CanceledAt { get; set; }
	}

	public enum ReactionType
	{
		Like,
		Dislike
	}
}