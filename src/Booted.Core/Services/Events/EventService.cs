using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Botted.Core.Abstractions.Services.Events;

namespace Booted.Core.Services.Events
{
	public class EventService : IEventService
	{
		private readonly Dictionary<Type, List<MulticastDelegate>> _events = new();

		[SuppressMessage("ReSharper", "MemberCanBeProtected.Global")]
		public EventService(IEnumerable<IEvent> events)
		{
			foreach (var @event in events)
			{
				_events.Add(@event.GetType(), new List<MulticastDelegate>());
			}
		}

		public virtual void Raise<TEvent>() 
			where TEvent : IEvent
		{
			foreach (var handler in _events[typeof(TEvent)].OfType<Action>())
			{
				handler.Invoke();
			}
		}

		public virtual void Raise<TEvent, TData>(TData data) 
			where TEvent : IEvent<TData>
		{
			foreach (var handler in _events[typeof(TEvent)].OfType<Action<TData>>())
			{
				handler.Invoke(data);
			}
		}

		public IEventSubscription Subscribe<TEvent>(Action handler) 
			where TEvent : IEvent
			=> SubscribeTo<TEvent>(handler);

		public IEventSubscription Subscribe<TEvent, TData>(Action<TData> handler)
			where TEvent : IEvent<TData>
			=> SubscribeTo<TEvent>(handler);

		private IEventSubscription SubscribeTo<TEvent>(MulticastDelegate handler)
		{
			var subscription = new EventSubscription();
			subscription.Unsubscribed += () => Unsubscribe(typeof(TEvent), handler);
			_events[typeof(TEvent)].Add(handler);
			return subscription;
		}

		private void Unsubscribe(Type @event, MulticastDelegate handler)
		{
			_events[@event].Remove(handler);
		} 
	}
}