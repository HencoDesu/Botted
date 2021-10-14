using Autofac;
using Botted.Core.Abstractions.Extensions;
using Botted.Core.Commands.Abstractions.Data;

namespace Botted.Core.Commands.Abstractions.Extensions
{
	public static class BotBuilderExtensions
	{

		/// <summary>
		/// Register command to bot
		/// </summary>
		/// <param name="builder">Current bot builder</param>
		/// <typeparam name="TCommand">Command to register</typeparam>
		/// <returns>Current <see cref="ContainerBuilder"/></returns>
		public static ContainerBuilder RegisterCommand<TCommand>(this ContainerBuilder builder)
			where TCommand : ICommand<EmptyCommandData>
		{
			return builder.RegisterCommand<TCommand, EmptyCommandData>();
		}
		
		/// <summary>
		/// Register command to bot
		/// </summary>
		/// <param name="builder">Current bot builder</param>
		/// <typeparam name="TCommand">Command to register</typeparam>
		/// <typeparam name="TData">Command data type</typeparam>
		/// <returns>Current <see cref="ContainerBuilder"/></returns>
		public static ContainerBuilder RegisterCommand<TCommand, TData>(this ContainerBuilder builder)
			where TCommand : ICommand<TData>
			where TData : class, ICommandData
		{
			return builder.RegisterSingleton<TCommand>()
						  .RegisterBuildCallback(RegisterCommand<TCommand, TData>);
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