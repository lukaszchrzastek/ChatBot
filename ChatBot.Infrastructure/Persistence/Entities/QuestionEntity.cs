using System.ComponentModel.DataAnnotations;

namespace ChatBot.Infrastructure.Persistence.Entities
{
	public class QuestionEntity
	{
		[Key]
		public Guid Id { get; set; }
		public string Text { get; set; }
		public DateTime CreatedAt { get; set; }
		public AnswerEntity Answer { get; set; }
		public Guid UserEntityId { get; set; }
		public UserEntity User { get; set; }
	}
}
