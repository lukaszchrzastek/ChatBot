using ChatBot.Domain.ValueObjects;

namespace ChatBot.Domain.Models
{
	public class Answer
	{
		public Guid Id { get; private set; }
		public string Text { get; private set; }
		public DateTime CreatedAt { get; private set; }
		public ReactionType? Reaction { get; private set; }
		public DateTime? CanceledAt { get; private set; }

		private Answer() { }
		public static Answer Create(
			Guid id,
			string text,
			DateTime createdAt,
			ReactionType? reaction = null,
			DateTime? canceledAt = null)
		{
			return new Answer
			{
				Id = id,
				Text = text,
				CreatedAt = createdAt,
				Reaction = reaction,
				CanceledAt = canceledAt
			};
		}

		public void SetReaction(ReactionType reaction)
		{
			Reaction = reaction;
		}

		public void Cancel(DateTime canceledAt)
		{
			if (CanceledAt.HasValue)
				throw new InvalidOperationException("Odpowiedź została już anulowana.");
			CanceledAt = canceledAt;
		}
	}
}
