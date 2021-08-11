using System;

namespace Booted.Core.Events.Abstractions
{
	public interface IEventService
	{
		void Raise<TEvent>()
			where TEvent : IEvent;

		void Raise<TEvent, TData>(TData data) 
			where TEvent : IEvent<TData>;
		
		IEventSubscription Subscribe<TEvent>(Action handler) 
			where TEvent : IEvent;
		
		IEventSubscription Subscribe<TEvent, TData>(Action<TData> handler) 
			where TEvent : IEvent<TData>;
	}
}