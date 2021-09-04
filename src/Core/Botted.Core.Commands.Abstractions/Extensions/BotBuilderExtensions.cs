using Autofac;
using Botted.Core.Abstractions.Builders;
using Botted.Core.Abstractions.Extensions;
using Botted.Core.Commands.Abstractions.Data;

namespace Botted.Core.Commands.Abstractions.Extensions
{
	public static class BotBuilderExtensions
	{
		/// <summary>
		/// Register selected implementation of <see cref="ICommandService"/> as common
		/// </summary>
		/// <param name="builder">Current bot builder</param>
		/// <typeparam name="TCommandService">Implementation of command service</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder UseCommandService<TCommandService>(this IBotBuilder builder)
			where TCommandService : ICommandService
		{
			return builder.RegisterService<ICommandService, TCommandService>();
		}

		/// <summary>
		/// Register selected implementation of <see cref="ICommandParser"/> as common
		/// </summary>
		/// <param name="builder">Current bot builder</param>
		/// <typeparam name="TParser">Implementation of parser</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder UseCommandParser<TParser>(this IBotBuilder builder)
			where TParser : ICommandParser
		{
			return builder.RegisterSingleton<ICommandParser, TParser>();
		}

		/// <summary>
		/// Register command to bot
		/// </summary>
		/// <param name="builder">Current bot builder</param>
		/// <typeparam name="TCommand">Command to register</typeparam>
		/// <typeparam name="TData">Command data type</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder RegisterCommand<TCommand, TData>(this IBotBuilder builder)
			where TCommand : ICommand<TData>
			where TData : class, ICommandData
		{
			return builder.RegisterSingleton<TCommand>()
						  .ConfigureServices(b => b.RegisterBuildCallback(RegisterCommand<TCommand, TData>));
		}

		private static void RegisterCommand<TCommand, TData>(IComponentContext componentContext)
			where TCommand : ICommand<TData>
			where TData : class, ICommandData
		{
			var commandService = componentContext.Resolve<ICommandService>();
			var command = componentContext.Resolve<TCommand>();
			commandService.RegisterCommand(command);
		}
	}
}