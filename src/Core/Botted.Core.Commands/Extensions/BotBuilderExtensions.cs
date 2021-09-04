using Botted.Core.Abstractions.Builders;
using Botted.Core.Commands.Abstractions;
using Botted.Core.Commands.Abstractions.Extensions;

namespace Botted.Core.Commands.Extensions
{
	public static class BotBuilderExtensions
	{
		/// <summary>
		/// Register default implementation of <see cref="ICommandService"/> as common
		/// </summary>
		/// <param name="builder">Current bot builder</param>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder UseDefaultCommandService(this IBotBuilder builder)
			=> builder.UseCommandService<CommandService>();
		
		/// <summary>
		/// Register default implementation of <see cref="ICommandParser"/> as common
		/// </summary>
		/// <param name="builder">Current bot builder</param>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder UseDefaultCommandParser(this IBotBuilder builder)
			=> builder.UseCommandParser<CommandParser>();

		
	}
}