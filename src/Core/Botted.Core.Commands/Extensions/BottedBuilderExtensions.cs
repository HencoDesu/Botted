using Botted.Core.Abstractions;
using Botted.Core.Commands.Abstractions.Extensions;

namespace Botted.Core.Commands.Extensions
{
	public static class BottedBuilderExtensions
	{
		public static IBottedBuilder UseDefaultCommandService(this IBottedBuilder bottedBuilder)
		{
			return bottedBuilder.UseCommandService<CommandBottedService>()
								.ConfigureServices(c => c.RegisterConfigurationSection<CommandsConfiguration>("Commands"));
		}

		public static IBottedBuilder UseDefaultCommandParser(this IBottedBuilder bottedBuilder)
		{
			return bottedBuilder.UseCommandParser<CommandParser>();
		}
	}
}