namespace ChatBot.API.Dtos
{
	public class QuestionDto
	{
		public Guid Id { get; set; }
		public required string Text { get; set; }
		public DateTime CreatedAt { get; set; }
		public AnswerDto? Answer { get; set; }
	}
}