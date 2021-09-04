using System.Threading.Tasks;
using Botted.Core.Abstractions;
using Botted.Core.Events.Abstractions.Delegates;
using Botted.Core.Events.Abstractions.Events;

namespace Botted.Core.Events.Abstractions
{
	/// <summary>
	/// Manages all events that can occurs in bot
	/// </summary>
	public interface IEventService : IService
	{
		/// <summary>
		/// Fires all subscribed handlers
		/// </summary>
		/// <typeparam name="TEvent">Event type</typeparam>
		void Raise<TEvent>() 
			where TEvent : Event;

		/// <summary>
		/// Fires all subscribed handlers and wait for results
		/// </summary>
		/// <typeparam name="TEvent">Event type</typeparam>
		/// <returns>Task with results</returns>
		Task RaiseAndWait<TEvent>()
			where TEvent : Event;

		/// <summary>
		/// Fires all subscribed handlers
		/// </summary>
		/// <param name="data">Event data</param>
		/// <typeparam name="TEvent">Event type</typeparam>
		/// <typeparam name="TData">Event data type</typeparam>
		void Raise<TEvent, TData>(TData data) 
			where TEvent : EventWithData<TData>;

		/// <summary>
		/// Fires all subscribed handlers and wait for results
		/// </summary>
		/// <param name="data">Event data</param>
		/// <typeparam name="TEvent">Event type</typeparam>
		/// <typeparam name="TData">Event data type</typeparam>
		Task RaiseAndWait<TEvent, TData>(TData data)
			where TEvent : EventWithData<TData>;

		/// <summary>
		/// Subscribe handler to specific <see cref="Event"/>
		/// This handler will be executed on <see cref="Raise{TEvent}"/>
		/// or <see cref="RaiseAndWait{TEvent}"/> call
		/// </summary>
		/// <param name="handler">Handler for event</param>
		/// <typeparam name="TEvent">Event type</typeparam>
		/// <returns><see cref="IEventSubscription"/></returns>
		IEventSubscription Subscribe<TEvent>(BotEventHandler handler)
			where TEvent : Event, new();

		/// <summary>
		/// Subscribe handler to specific <see cref="Event"/>
		/// This handler will be executed on <see cref="Raise{TEvent}"/> call
		/// </summary>
		/// <param name="handler">Handler for event</param>
		/// <typeparam name="TEvent">Event type</typeparam>
		/// <returns><see cref="IEventSubscription"/></returns>
		IEventSubscription Subscribe<TEvent>(AsyncEventHandler handler)
			where TEvent : Event, new();

		/// <summary>
		/// Subscribe handler to specific <see cref="EventWithData{TData}"/>
		/// This handler will be executed on <see cref="Raise{TEvent,TData}"/>
		/// or <see cref="RaiseAndWait{TEvent, TData}"/> call
		/// </summary>
		/// <param name="handler">Handler for event</param>
		/// <typeparam name="TEvent">Event type</typeparam>
		/// <typeparam name="TData">Event data type</typeparam>
		/// <returns><see cref="IEventSubscription"/></returns>
		IEventSubscription Subscribe<TEvent, TData>(BotEventHandler<TData> handler)
			where TEvent : EventWithData<TData>, new();

		/// <summary>
		/// Subscribe handler to specific <see cref="EventWithData{TData}"/>
		/// This handler will be executed on <see cref="Raise{TEvent,TData}"/> call
		/// </summary>
		/// <param name="handler">Handler for event</param>
		/// <typeparam name="TEvent">Event type</typeparam>
		/// <typeparam name="TData">Event data type</typeparam>
		/// <returns><see cref="IEventSubscription"/></returns>
		IEventSubscription Subscribe<TEvent, TData>(AsyncEventHandler<TData> handler)
			where TEvent : EventWithData<TData>, new();
	}
}