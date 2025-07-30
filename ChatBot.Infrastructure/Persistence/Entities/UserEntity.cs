using System.ComponentModel.DataAnnotations;

namespace ChatBot.Infrastructure.Persistence.Entities
{
	public class UserEntity
	{
		[Key]
		public Guid Id { get; set; }
		public DateTime CreatedAt { get; set; }
		public List<QuestionEntity> Questions { get; set; } = [];
	}
}
