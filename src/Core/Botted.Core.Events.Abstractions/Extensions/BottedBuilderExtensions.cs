using Botted.Core.Abstractions;

namespace Botted.Core.Events.Abstractions.Extensions
{
	public static class BottedBuilderExtensions
	{
		public static IBottedBuilder UseEventService<TEventService>(this IBottedBuilder bottedBuilder)
			where TEventService : IEventService
		{
			return bottedBuilder.ConfigureContainer(c => c.RegisterService<IEventService, TEventService>());
		}
	}
}