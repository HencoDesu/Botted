using Botted.Core.Abstractions;
using Botted.Core.Events.Abstractions;
using Botted.Core.Events.Abstractions.Extensions;

namespace Botted.Core.Events.Extensions
{
	public static class BotBuilderExtensions
	{
		/// <summary>
		/// Register default implementation of <see cref="IEventService"/> as common
		/// </summary>
		/// <param name="builder">Current bot builder</param>
		/// <returns>Current <see cref="IBotBuilder"/></returns>
		public static IBotBuilder UseDefaultEventService(this IBotBuilder builder)
			=> builder.UseEventService<EventService>();
	}
}