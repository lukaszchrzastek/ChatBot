namespace ChatBot.API.Middlewares
{
	public class UserIdMiddleware(RequestDelegate next)
	{
		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Cookies.TryGetValue("user-id", out var userId))
			{
				context.Items["UserId"] = userId;
			}

			await next(context);
		}
	}
}