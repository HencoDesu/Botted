using Botted.Core.Abstractions;
using Botted.Core.Commands.Abstractions.Data;

namespace Botted.Core.Commands.Abstractions.Extensions
{
	public static class BottedBuilderExtensions
	{
		public static IBottedBuilder UseCommandService<TCommandService>(this IBottedBuilder bottedBuilder)
			where TCommandService : ICommandService
		{
			return bottedBuilder.ConfigureContainer(c => c.RegisterService<ICommandService, TCommandService>());
		}

		public static IBottedBuilder UseCommandParser<TCommandParser>(this IBottedBuilder bottedBuilder)
			where TCommandParser : ICommandParser
		{
			return bottedBuilder.ConfigureContainer(c => c.RegisterSingleton<ICommandParser, TCommandParser>());
		}

		public static IBottedBuilder RegisterCommand<TCommand>(this IBottedBuilder bottedBuilder)
			where TCommand : ICommand<EmptyCommandData>
		{
			return bottedBuilder.RegisterCommand<TCommand, EmptyCommandData>();
		}

		public static IBottedBuilder RegisterCommand<TCommand, TData>(this IBottedBuilder bottedBuilder)
			where TCommand : ICommand<TData> 
			where TData : class, ICommandData
		{
			return bottedBuilder.ConfigureContainer(c => c.RegisterBuildCallback(container =>
			{
				var commandService = container.Resolve<ICommandService>();
				var command = container.Resolve<TCommand>();
				commandService.RegisterCommand(command);
			}));
		}
	}
}