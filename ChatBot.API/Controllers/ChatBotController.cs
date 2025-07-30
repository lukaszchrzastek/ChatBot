using ChatBot.API.Dtos;
using ChatBot.Application.Features.Questions.Commands;
using ChatBot.Application.Features.Questions.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ChatBot.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ChatBotController(
		IMediator mediator
		) : ControllerBase
	{
		private Guid GetUserId()
		{
			var userId = HttpContext.Items["UserId"]?.ToString();
			if (Guid.TryParse(userId, out var parsedUserId))
				return parsedUserId;

			throw new UnauthorizedAccessException("Użytkownik niezautoryzowany");
		}

		[HttpPost("add-question")]
		public async Task<ActionResult<QuestionDto>> AddQuestion([FromBody] AddQuestionDto dto, CancellationToken cancellationToken)
		{
			var userId = GetUserId();
			var question = await mediator.Send(new AddQuestionCommand(userId, dto.QuestionText), cancellationToken);
			return Ok(Mappers.QuestionMapper.MapFromDomain(question));
		}

		[HttpPost("cancel-question")]
		public async Task<IActionResult> CancelQuestion([FromBody] CancelQuestionDto dto, CancellationToken cancellationToken)
		{
			var userId = GetUserId();
			await mediator.Send(new CancelQuestionCommand(dto.QuestionId, userId), cancellationToken);
			return NoContent();
		}

		[HttpGet("questions")]
		public async Task<ActionResult<List<QuestionDto>>> GetUserQuestions()
		{
			var userId = GetUserId();
			var result = await mediator.Send(new GetQuestionsByUserIdQuery(userId));
			var questions = result.Select(Mappers.QuestionMapper.MapFromDomain).ToList();
			return Ok(questions.OrderBy(x => x.CreatedAt));
		}

		[HttpPost("rate-answer")]
		public async Task<IActionResult> RateAnswer([FromBody] RateAnswerDto dto, CancellationToken cancellationToken)
		{
			try
			{
				var userId = GetUserId();
				var reaction = (Domain.ValueObjects.ReactionType)(int)dto.Reaction;
				await mediator.Send(new RateAnswerCommand(dto.QuestionId, userId, reaction), cancellationToken);
				return NoContent();
			}
			catch (Exception ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}
	}
}
