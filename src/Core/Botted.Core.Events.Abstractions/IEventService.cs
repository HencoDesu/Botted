using System;
using Botted.Core.Abstractions;
using Botted.Core.Events.Abstractions.Events;

namespace Botted.Core.Events.Abstractions
{
	/// <summary>
	/// Manages all events that can occurs in bot
	/// </summary>
	public interface IEventService : IService
	{
		IObservable<TEvent> GetEvent<TEvent>() where TEvent : Event;

		/// <summary>
		/// Fires all subscribed handlers
		/// </summary>
		/// <typeparam name="TEvent">Event type</typeparam>
		void Raise<TEvent>() 
			where TEvent : Event, new();

		/// <summary>
		/// Fires all subscribed handlers
		/// </summary>
		/// <param name="data">Event data</param>
		/// <typeparam name="TEvent">Event type</typeparam>
		/// <typeparam name="TData">Event data type</typeparam>
		void Raise<TEvent, TData>(TData data) 
			where TEvent : EventWithData<TData>, new();
	}
}