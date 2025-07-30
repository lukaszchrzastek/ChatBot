namespace ChatBot.Domain.Models
{
	public class User
	{
		public Guid Id { get; private set; }
		public DateTime CreatedAt { get; private set; }

		private User() { }
		public static User Create(
			Guid id,
			DateTime createdAt)
		{
			return new User
			{
				Id = id,
				CreatedAt = createdAt
			};
		}
	}
}
