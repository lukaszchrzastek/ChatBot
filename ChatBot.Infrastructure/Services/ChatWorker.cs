using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace ChatBot.Infrastructure.Services
{
	public class ChatWorker(IServiceScopeFactory scopeFactory, ILogger<ChatWorker> logger) : IChatWorker
	{
		private readonly ILogger<ChatWorker> _logger = logger;

		private readonly ConcurrentDictionary<Guid, CancellationTokenSource> _tasks = new();

		private readonly IServiceScopeFactory _scopeFactory = scopeFactory;

		public void AddTask(Guid userId, Guid questionId, string questionText)
		{
			var cts = new CancellationTokenSource();
			if (!_tasks.TryAdd(questionId, cts))
			{
				_logger.LogWarning("Zadanie {QuestionId} już istnieje.", questionId);
				return;
			}

			var token = cts.Token;

			_ = Task.Run(async () =>
			{
				try
				{
					using var scope = _scopeFactory.CreateScope();
					var runner = scope.ServiceProvider.GetRequiredService<IChatTaskRunner>();
					await runner.RunAsync(userId, questionId, questionText, token);
				}
				catch (Exception ex)
				{
					_logger.LogError(ex, "Błąd podczas wykonywania zadania {QuestionId}.");
				}
				finally
				{
					_tasks.TryRemove(questionId, out _);
				}
			}, token);
		}

		public void CancelTask(Guid questionId)
		{
			if (_tasks.TryRemove(questionId, out var cts))
			{
				cts.Cancel();
				_logger.LogInformation("Anulowano zadanie {QuestionId}.", questionId);
			}
			else
			{
				_logger.LogWarning("Nie znaleziono zadania {QuestionId} do anulowania.", questionId);
			}
		}

		public void CancelAllTasks()
		{
			foreach (var kvp in _tasks)
			{
				kvp.Value.Cancel();
			}

			_tasks.Clear();
			_logger.LogInformation("Wszystkie zadania zostały anulowane.");
		}
	}
}
