using ChatBot.Domain.Models;
using ChatBot.Domain.Services;
using System.Threading.Channels;

namespace ChatBot.Infrastructure.Hubs
{
	public class ChatQueue : IChatQueue
	{
		private readonly Channel<ChatMessage> _channel = Channel.CreateUnbounded<ChatMessage>();
		public ChannelReader<ChatMessage> Reader => _channel.Reader;
		public ValueTask QueueAsync(ChatMessage message) => _channel.Writer.WriteAsync(message);
	}
}