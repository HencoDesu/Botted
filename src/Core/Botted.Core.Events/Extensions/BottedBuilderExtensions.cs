using Botted.Core.Abstractions;
using Botted.Core.Events.Abstractions.Extensions;

namespace Botted.Core.Events.Extensions
{
	public static class BottedBuilderExtensions
	{
		public static IBottedBuilder UseDefaultEventService(this IBottedBuilder bottedBuilder)
		{
			return bottedBuilder.UseEventService<EventService>();
		}
	}
}