using ChatBot.Domain.Services;
using System.Threading.Channels;

namespace ChatBot.Infrastructure.Services
{
	public class FakeAiChatService : IAiChatService
	{
		public ChannelReader<string> Ask(string question, CancellationToken cancellationToken)
		{
			var channel = Channel.CreateUnbounded<string>();
			var writer = channel.Writer;

			_ = Task.Run(async () =>
			{
				var random = new Random();
				int iterationLimit = random.Next(1, 10);

				try
				{
					for (int i = 0; i < iterationLimit && !cancellationToken.IsCancellationRequested; i++)
					{
						int delayMs = random.Next(1000, 5001);
						int textLength = random.Next(50, 501);
						string text = GenerateRandomText(textLength);

						await writer.WriteAsync(text, cancellationToken);
						await Task.Delay(delayMs, cancellationToken);
					}
				}
				catch (OperationCanceledException)
				{
					await writer.WriteAsync("Zadanie zostało anulowane.", CancellationToken.None);
				}
				finally
				{
					writer.Complete();
				}
			}, cancellationToken);

			return channel.Reader;
		}

		private static string GenerateRandomText(int length)
		{
			const string sourceText = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua";

			var random = new Random();
			char[] buffer = new char[length];

			for (int i = 0; i < length; i++)
			{
				buffer[i] = sourceText[random.Next(sourceText.Length)];
			}

			return new string(buffer);
		}
	}
}