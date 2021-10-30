using Botted.Core.Abstractions;

namespace Botted.Core.Events.Abstractions.Extensions
{
	public static class BottedBuilderExtensions
	{
		public static IBottedBuilder UseEventService<TEventService>(this IBottedBuilder bottedBuilder)
			where TEventService : IEventBottedService
		{
			return bottedBuilder.ConfigureServices(c => c.RegisterService<IEventBottedService, TEventService>());
		}
	}
}