using System.Threading.Channels;

namespace ChatBot.Domain.Services
{
	public interface IAiChatService
	{
		ChannelReader<string> Ask(string question, CancellationToken token);
	}
}