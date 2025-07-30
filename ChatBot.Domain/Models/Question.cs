using ChatBot.Domain.ValueObjects;

namespace ChatBot.Domain.Models
{
	public class Question
	{
		public Guid Id { get; private set; }
		public string Text { get; private set; }
		public DateTime CreatedAt { get; private set; }
		public Answer Answer { get; private set; }

		private Question() { }

		public static Question Create(
			Guid id,
			string text,
			DateTime createdAt)
		{
			return new Question { Id = id, Text = text, CreatedAt = createdAt };
		}

		public void AddAnswer(Answer answer)
		{
			ArgumentNullException.ThrowIfNull(answer);

			Answer = answer;
		}

		public void RateAnswer(ReactionType reaction)
		{
			if (Answer == null)
				throw new InvalidOperationException("No answer to rate.");

			Answer.SetReaction(reaction);
		}

		public void Cancel(DateTime canceledAt)
		{
			if (Answer == null)
				throw new InvalidOperationException("No answer to cancel.");

			Answer.Cancel(canceledAt);
		}
	}
}