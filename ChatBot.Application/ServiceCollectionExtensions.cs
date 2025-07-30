using ChatBot.Application.Features.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace ChatBot.Application
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddApplication(this IServiceCollection services)
		{
			var applicationAssembly = typeof(Features.Questions.Commands.AddQuestionCommand).Assembly;

			services.AddValidatorsFromAssemblyContaining<AddQuestionCommandValidator>();
			services.AddValidatorsFromAssemblyContaining<CancelQuestionCommandValidator>();
			services.AddValidatorsFromAssemblyContaining<RateAnswerCommandValidator>();

			services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ChatBot.Application.Common.Behaviors.ValidationBehavior<,>));

			return services;
		}
	}
}
