using FluentValidation;
using System.Text.Json;

namespace ChatBot.API.Middlewares
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly ILogger<ExceptionMiddleware> _logger;

		public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
		{
			_next = next;
			_logger = logger;
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await _next(context);
			}
			catch (ValidationException ex)
			{
				context.Response.StatusCode = 400;
				context.Response.ContentType = "application/json";

				var errors = ex.Errors
					.GroupBy(e => e.PropertyName)
					.ToDictionary(
						g => g.Key,
						g => g.Select(e => e.ErrorMessage).ToArray()
					);

				var result = JsonSerializer.Serialize(new { errors });
				await context.Response.WriteAsync(result);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Unexpected error");
				context.Response.StatusCode = 500;
				await context.Response.WriteAsync("Unexpected server error");
			}
		}
	}
}
