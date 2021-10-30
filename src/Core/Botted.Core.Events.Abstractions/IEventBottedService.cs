using Botted.Core.Abstractions;
using Botted.Core.Events.Abstractions.Events;

namespace Botted.Core.Events.Abstractions
{
	/// <summary>
	/// Manages all events that can occurs in bot
	/// </summary>
	public interface IEventBottedService : IBottedService
	{
		/// <summary>
		/// Gets any event for subscription or raising
		/// </summary>
		/// <typeparam name="TEvent">Event type</typeparam>
		/// <returns><see cref="Event"/></returns>
		TEvent GetEvent<TEvent>() 
			where TEvent : Event, new();
	}
}