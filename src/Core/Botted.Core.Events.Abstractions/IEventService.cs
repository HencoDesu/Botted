using System;

namespace Botted.Core.Events.Abstractions
{
	/// <summary>
	/// Manages all events that can occurs in bot
	/// </summary>
	public interface IEventService
	{
		/// <summary>
		/// Fires all subscribed handlers
		/// </summary>
		/// <typeparam name="TEvent">Event type</typeparam>
		void Raise<TEvent>() 
			where TEvent : IEvent;

		/// <summary>
		/// Fires all subscribed handlers
		/// </summary>
		/// <param name="data">Event data</param>
		/// <typeparam name="TEvent">Event type</typeparam>
		/// <typeparam name="TData">Event data type</typeparam>
		void Raise<TEvent, TData>(TData data) 
			where TEvent : IEventWithData<TData>;

		
		/// <summary>
		/// Subscribe handler to specific <see cref="IEvent"/>
		/// This handler will be executed on <see cref="Raise{TEvent}"/> call
		/// </summary>
		/// <param name="handler">Handler for event</param>
		/// <typeparam name="TEvent">Event type</typeparam>
		/// <returns>Event subscription with opportunity to unsubscribe later</returns>
		IEventSubscription Subscribe<TEvent>(Action handler) 
			where TEvent : IEvent, new();
		
		/// <summary>
		/// Subscribe handler to specific <see cref="IEvent"/>
		/// This handler will be executed on <see cref="Raise{TEvent,TData}"/> call
		/// </summary>
		/// <param name="handler">Handler for event</param>
		/// <typeparam name="TEvent">Event type</typeparam>
		/// <typeparam name="TData">Event data type</typeparam>
		/// <returns>Event subscription with opportunity to unsubscribe later</returns>
		IEventSubscription Subscribe<TEvent, TData>(Action<TData> handler) 
			where TEvent : IEventWithData<TData>, new();
	}
}