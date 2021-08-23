using Botted.Core.Abstractions;

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
			=> builder.RegisterService<ICommandService, TCommandService>();
		
		/// <summary>
		/// Register selected implementation of <see cref="ICommandParser"/> as common
		/// </summary>
		/// <param name="builder">Current bot builder</param>
		/// <typeparam name="TParser">Implementation of parser</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder UseCommandParser<TParser>(this IBotBuilder builder) 
			where TParser : ICommandParser 
			=> builder.RegisterService<ICommandParser, TParser>();
	}
}