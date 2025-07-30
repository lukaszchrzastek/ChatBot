namespace ChatBot.Infrastructure.Services
{
	public record PartialAnswerMessage(
		Guid QuestionId,
		Guid Id,
		int Sequence,
		string Text,
		bool IsFinalChunk = false);
}
