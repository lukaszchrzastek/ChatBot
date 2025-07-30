using System.ComponentModel.DataAnnotations;

namespace ChatBot.Infrastructure.Persistence.Entities
{
	public enum ReactionType
	{
		Like,
		Dislike
	}

	public class AnswerEntity
	{
		[Key]
		public Guid Id { get; set; }
		public string Text { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime? CanceledAt { get; set; }
		public Guid QuestionEntityId { get; set; }
		public ReactionType? Reaction { get; set; }
		public QuestionEntity Question { get; set; }
	}
}
