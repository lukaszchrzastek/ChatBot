using ChatBot.Domain.Models;
using System.Threading.Channels;

namespace ChatBot.Domain.Services
{
	public interface IChatQueue
	{
		ValueTask QueueAsync(ChatMessage message);
		ChannelReader<ChatMessage> Reader { get; }
	}
}