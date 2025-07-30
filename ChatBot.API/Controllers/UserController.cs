using ChatBot.API.Dtos;
using ChatBot.Application.Features.Questions.Commands;
using ChatBot.Application.Features.Questions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController(
		IMediator mediator
		) : ControllerBase
	{
		[HttpPost("create-user")]
		public async Task<ActionResult<UserDto>> CreateUser(CancellationToken cancellationToken)
		{
			var user = await mediator.Send(new CreateUserCommand(), cancellationToken);
			return Ok(new UserDto { Id = user.Id });
		}

		[HttpGet("get-user")]
		public async Task<ActionResult<UserDto>> GetUser(Guid userId, CancellationToken cancellationToken)
		{
			var user = await mediator.Send(new GetUserQuery(userId), cancellationToken);

			if (user == null)
			{
				return NotFound(new { error = "Użytkownik nie istnieje" });
			}

			return Ok(new UserDto { Id = user.Id });
		}
	}
}
