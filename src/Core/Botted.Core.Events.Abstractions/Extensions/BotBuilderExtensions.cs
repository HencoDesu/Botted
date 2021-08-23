using Botted.Core.Abstractions;

namespace Botted.Core.Events.Abstractions.Extensions
{
	public static class BotBuilderExtensions
	{
		/// <summary>
		/// Register selected implementation of <see cref="IEventService"/> as common
		/// </summary>
		/// <param name="builder">Current bot builder</param>
		/// <typeparam name="TEventService">Implementation of event service</typeparam>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder UseEventService<TEventService>(this IBotBuilder builder)
			where TEventService : IEventService
			=> builder.RegisterService<IEventService, TEventService>();
	}
}